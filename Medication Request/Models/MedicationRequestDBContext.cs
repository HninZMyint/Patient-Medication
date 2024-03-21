using Microsoft.EntityFrameworkCore;

namespace Medication_Request.Models
{
    public class MedicationRequestDBContext : DbContext
    {
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Medication> Medication { get; set; }
        public DbSet<Clinician> Clinician { get; set; }
        public DbSet<MedicationRequest> MedicationRequest { get; set; }

        public MedicationRequestDBContext(DbContextOptions options) : base(options) { }

        public MedicationRequestDBContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=localhost\\MSSQLSERVER01;Database=PatientMedication;Trusted_Connection=True;");
        }
    }
}
