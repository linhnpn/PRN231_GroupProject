using System.ComponentModel.DataAnnotations;

namespace GroupProject_HRM_Library.Models
{
    public class EmployeeProject
    {
        public int ProjectID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EmployeeProjectStatus { get; set; }
        public Employee? Employee { get; set; }
        public Project? Project { get; set; }
    }
}