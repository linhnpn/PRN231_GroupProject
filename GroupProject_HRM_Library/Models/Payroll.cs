using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GroupProject_HRM_Library.Models
{
    public class Payroll
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PayrollID { get; set; }
        public decimal IncomePerMonth { get; set; }
        public DateTime Timestamp { get; set; }
        public int EmployeeID { get; set; }
        public Employee? Employee { get; set; }
    }
}