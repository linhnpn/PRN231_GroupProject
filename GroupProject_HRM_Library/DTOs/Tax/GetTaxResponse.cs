using GroupProject_HRM_Library.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DTOs.Tax
{
    public class GetTaxResponse
    {
        public int TaxID { get; set; }
        public decimal SalaryMin { get; set; }
        public decimal SalaryMax { get; set; }
        public double Percent { get; set; }
        public DateTime Timestamp { get; set; }
        public TaxEnum.TaxStatus TaxStatus { get; set; }
    }
}
