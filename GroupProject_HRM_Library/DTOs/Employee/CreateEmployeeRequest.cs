using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DTOs.Employee
{
    public class CreateEmployeeRequest
    {
        [Required(ErrorMessage = "Employee ID is required")]
        public string EmployeeName { get; set; }
        [Required(ErrorMessage = "EmployeeImage is required")]
        public string EmployeeImage { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "City is invalid")]
        public string Address { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email is invalid")]
        public string EmailAddress { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int RoleID { get; set; }
    }
}
