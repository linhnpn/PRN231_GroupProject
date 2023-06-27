using GroupProject_HRM_Library.DTOs.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DTOs.Income
{
    public class GetIncomeEmployeeResponse
    {
        public int IncomeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PreviousRentIncome { get; set; }
        public decimal AfterRentIncome { get; set; }
    }
}
