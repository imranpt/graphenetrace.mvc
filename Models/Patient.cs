namespace Project.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public int UserId { get; set; }            // links to Users table
        public User User { get; set; }
        public string? Name { get; set; }
        public string Notes { get; set; }
        public string? Condition { get; set; }
        public string? ClinicianNotes { get; set; }
        public int? AssignedClinicianId { get; set; }
    }
}
