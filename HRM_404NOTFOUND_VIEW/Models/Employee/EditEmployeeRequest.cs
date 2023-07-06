using System.ComponentModel.DataAnnotations;

namespace GroupProject_HRM_View.Models.Employee
{
    public class EditEmployeeRequest
    {
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public string EmployeeImage { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public int EmployeeStatus { get; set; }
        [Required]
        public int RoleID { get; set; }
    }
}
