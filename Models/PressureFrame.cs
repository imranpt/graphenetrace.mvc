namespace Project.Models
{
    public class PressureFrame
    {
        public int Id { get; set; }

        // Link to patient
        public int PatientId { get; set; }
      
        // Timestamp of this pressure frame
        public DateTime Timestamp { get; set; }

        // Metrics
        public int PeakPressure { get; set; }
        public double ContactAreaPercent { get; set; }

        // Store 32x32 pressure matrix as JSON text
        public string MatrixJson { get; set; }
    }
}
