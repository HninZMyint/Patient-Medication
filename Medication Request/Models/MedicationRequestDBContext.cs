using Microsoft.EntityFrameworkCore;

namespace Medication_Request.Models
{
    public class MedicationRequestDBContext : DbContext
    {
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<Medication> Medication { get; set; }
        public virtual DbSet<Clinician> Clinician { get; set; }
        public virtual DbSet<MedicationRequest> MedicationRequest { get; set; }

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
