using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medication_Request.Models
{
    [Table("Patient")]
    public class Patient
    {
        public enum PatientSex { male, female };

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public PatientSex Sex { get; set; }
    }
}
