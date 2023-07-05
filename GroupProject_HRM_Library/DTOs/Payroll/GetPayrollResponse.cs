using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DTOs.Payroll
{
    public class GetPayrollResponse
    {
        public int PayrollID { get; set; }
        public decimal IncomePerMonth { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
