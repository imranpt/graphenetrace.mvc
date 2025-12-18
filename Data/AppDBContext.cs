namespace Project.Data
{
    using Microsoft.EntityFrameworkCore;
    using Project.Models;

    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PressureFrame> PressureFrames { get; set; }

    }
}
