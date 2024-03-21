using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medication_Request.Models
{
    [Table("Clinician")]
    public class Clinician
    {
        public string RegistrationId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
