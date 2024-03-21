using System.ComponentModel.DataAnnotations.Schema;

namespace Medication_Request.Models
{
    [Table("Medication")]
    public class Medication
    {
        public enum MedicineForm { powder, tablet, capsule, syrup };

        public string Code { get; set; }

        public string CodeName { get; set; }

        public string CodeSystem { get; set; }

        public int StrengthValue { get; set; }

        public string StrengthUnit { get; set; }

        public MedicineForm Form { get; set; }
    }
}
