using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.Models
{
    public class Tax
    {
        public int TaxID { get; set; }
        public decimal SalaryMin { get; set; }
        public decimal SalaryMax { get; set; }
        public double Percent { get; set; }
        public DateTime Timestamp { get; set; }
        public int TaxStatus { get; set; }
    }
}
