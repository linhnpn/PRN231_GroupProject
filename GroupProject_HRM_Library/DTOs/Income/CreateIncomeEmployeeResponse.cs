using GroupProject_HRM_Library.DTOs.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DTOs.Income
{
    public class CreateIncomeEmployeeResponse
    {
        [Required(ErrorMessage = "Employee ID is required")]
        public int EmployeeID { get; set; }
    }
}
