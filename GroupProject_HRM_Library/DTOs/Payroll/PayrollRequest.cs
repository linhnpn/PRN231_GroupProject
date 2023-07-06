using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DTOs.Payroll
{
    public class PayrollRequest
    {
        [Required]
        public decimal IncomePerMonth { get; set; }
        [Required]
        public int EmployeeID { get; set; }
    }
}
