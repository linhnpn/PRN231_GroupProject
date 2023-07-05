using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GroupProject_HRM_Library.Models
{
    public class OvertimeLog
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OvertimeID { get; set; }
        public DateTime OverTimeDate { get; set; }
        public DateTime LogDate { get; set; }
        public int Hours { get; set; }
        public int OvertimeLogStatus { get; set; }
        public string? Description { get; set; }
        public int EmployeeID { get; set; }
        public Employee? Employee { get; set; }

    }
}