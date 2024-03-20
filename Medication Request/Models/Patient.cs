using System.ComponentModel.DataAnnotations;

namespace Medication_Request.Models
{
    public class Patient
    {
        public enum PatientSex { male, female };

        [Required]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public PatientSex Sex { get; set; }
    }
}
