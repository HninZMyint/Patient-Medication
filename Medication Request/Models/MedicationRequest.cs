using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medication_Request.Models
{
    [Table("MedicationRequest")]
    public class MedicationRequest
    {
        public enum RequestStatus { active, onhold, cancelled, completed };

        public string PatientReference { get; set; }

        public string ClinicianReference { get; set; }

        public string MedicationReference { get; set; }

        public string ReasonText { get; set; }

        [Required]
        public string PrescribedDate { get; set; }

        [Required]
        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public int Frequency { get; set; }

        public RequestStatus Status { get; set ; }
    }
}
