using System;
using System.Collections.Generic;

namespace Project.Models
{
    public class ClinicianPatientViewModel
    {
        public Patient Patient { get; set; }
        public List<PressureFrame> Frames { get; set; } = new();
        public int PeakPressure { get; set; }
        public double ContactArea { get; set; }
        public string RiskLevel { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string LatestMatrixJson { get; set; }
    }
}

