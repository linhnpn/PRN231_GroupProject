using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GroupProject_HRM_Library.Models
{
    public class Income
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IncomeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PreviousRentIncome { get; set; }
        public decimal AfterRentIncome { get; set; }
        public int EmployeeID { get; set; }
        public Employee? Employee { get; set; }        
    }
}