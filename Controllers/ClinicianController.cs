using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Models;
using Project.Services;
using System.Linq;

namespace Project.Controllers
{
    public class ClinicianController : Controller
    {
        private readonly AppDBContext _context;

        public ClinicianController(AppDBContext context)
        {
            _context = context;
        }

        private string CalculateRiskLevel(int peak)
        {
            if (peak > 200) return "High";
            if (peak > 120) return "Medium";
            return "Low";
        }

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("role") != "Clinician")
                return Unauthorized();

            var patients = _context.Patients.ToList();

            var vmList = patients.Select(p =>
            {
                var lastFrame = _context.PressureFrames
                    .Where(f => f.PatientId == p.Id)
                    .OrderByDescending(f => f.Timestamp)
                    .FirstOrDefault();

                return new ClinicianPatientViewModel
                {
                    Patient = p,
                    PeakPressure = lastFrame?.PeakPressure ?? 0,
                    ContactArea = lastFrame?.ContactAreaPercent ?? 0,
                    RiskLevel = lastFrame != null ? CalculateRiskLevel(lastFrame.PeakPressure) : "Unknown",
                    LastUpdated = lastFrame?.Timestamp
                };
            }).ToList();

            return View(vmList);   
        }


        public IActionResult Detail(int id)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.Id == id);
            if (patient == null) return NotFound();

            var frames = _context.PressureFrames
                .Where(f => f.PatientId == id)
                .OrderByDescending(f => f.Timestamp)
                .ToList();

            var vm = new ClinicianPatientViewModel
            {
                Patient = patient,
                Frames = frames,
                PeakPressure = frames.Any() ? frames.Max(f => f.PeakPressure) : 0,
                ContactArea = frames.Any() ? frames.Max(f => f.ContactAreaPercent) : 0,
                RiskLevel = frames.Any() ? CalculateRiskLevel(frames.Max(f => f.PeakPressure)) : "Unknown",
                LastUpdated = frames.Any() ? frames.First().Timestamp : null,
                LatestMatrixJson = frames.Any() ? frames.First().MatrixJson : "[]"
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult UploadCsv(IFormFile file, int patientId, [FromServices] PressureDataService processor)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file selected.");

            string folder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            Directory.CreateDirectory(folder);

            string filePath = Path.Combine(folder, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var frame = processor.ProcessCsv(filePath, patientId);

            _context.PressureFrames.Add(frame);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }
    }
}
