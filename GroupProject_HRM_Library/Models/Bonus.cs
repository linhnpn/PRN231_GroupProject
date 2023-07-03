using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GroupProject_HRM_Library.Models
{
    public class Bonus
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BonusID { get; set; }
        public string? Description { get; set; }
        public decimal BonusValue { get; set; }
        public DateTime Timestamp { get; set; }
        public int EmployeeID { get; set; }
        public Employee? Employee { get; set; }
    }
}