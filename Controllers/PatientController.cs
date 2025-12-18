using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Models;
using Microsoft.AspNetCore.Http;

namespace Project.Controllers
{
    public class PatientController : Controller
    {
        private readonly AppDBContext _context;

        public PatientController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Patients.ToList());
        }

        // CREATE GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [HttpPost]
        public IActionResult Create(Patient patient)
        {
            if (!ModelState.IsValid)
                return View(patient);

            _context.Patients.Add(patient);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // EDIT GET
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var p = _context.Patients.Find(id);

            if (p == null)
                return NotFound();  // FIXES NULL WARNING

            return View(p);
        }

        // EDIT POST
        [HttpPost]
        public IActionResult Edit(Patient patient)
        {
            if (!ModelState.IsValid)
                return View(patient);

            _context.Patients.Update(patient);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // DELETE GET
        public IActionResult Delete(int id)
        {
            var p = _context.Patients.Find(id);

            if (p == null)
                return NotFound();  // FIXES NULL WARNING

            return View(p);
        }

        // DELETE POST
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var p = _context.Patients.Find(id);

            if (p == null)
                return NotFound();  // FIXES CS8604

            _context.Patients.Remove(p);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Dashboard()
        {
            int? patientId = HttpContext.Session.GetInt32("userId");

            if (patientId == null)
                return RedirectToAction("Login", "Auth");

            var patient = _context.Patients.FirstOrDefault(p => p.Id == patientId);

            if (patient == null)
                return NotFound();

            // MATCH CLINICIAN: NEWEST → OLDEST
            var frames = _context.PressureFrames
                .Where(f => f.PatientId == patientId)
                .OrderByDescending(f => f.Timestamp)
                .ToList();

            var latest = frames.FirstOrDefault(); // NEWEST FRAME

            var vm = new PatientDashboardViewModel
            {
                Patient = patient,
                Frames = frames,               // list still descending, like clinician
                LatestPeakPressure = latest?.PeakPressure ?? 0,
                LatestContactArea = latest?.ContactAreaPercent ?? 0,
                RiskLevel = latest != null && latest.PeakPressure > 200 ? "High"
                           : latest != null && latest.PeakPressure > 120 ? "Medium"
                           : "Low",
                LatestMatrixJson = latest?.MatrixJson ?? "[]",
                LastUpdated = latest?.Timestamp
            };

            return View(vm);
        }





    }
}
