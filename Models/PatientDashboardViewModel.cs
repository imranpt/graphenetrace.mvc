namespace Project.Models
{
    public class PatientDashboardViewModel
    {
        public Patient Patient { get; set; }
        public List<PressureFrame> Frames { get; set; } = new();
        public int LatestPeakPressure { get; set; }
        public double LatestContactArea { get; set; }
        public string RiskLevel { get; set; }
        public string LatestMatrixJson { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
