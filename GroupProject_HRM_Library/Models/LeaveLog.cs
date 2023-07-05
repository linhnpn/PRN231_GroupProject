using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GroupProject_HRM_Library.Models
{
    public class LeaveLog
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeaveLogID { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Reason { get; set; }
        public string? LinkProof { get; set; }
        public int LeaveLogStatus { get; set; }
        public string? RejectReson { get; set; }
        public int EmployeeID { get; set; }
        public Employee? Employee { get; set; }
    }
}