using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupProject_HRM_Library.Enums;

namespace GroupProject_HRM_Library.DTOs.Tax
{
    public class UpdateTaxRequest
    {
        [Required(ErrorMessage = "Minimum Salary is required")]
        [DisplayName("Minimum Salary")]
        [DataType(DataType.Currency)]
        [Range(0, 999999999999999999.99, ErrorMessage = "Minimum Salary range from {2} to {1} dolars.")]
        public decimal SalaryMin { get; set; }
        [Required(ErrorMessage = "Maximum Salary is required")]
        [DisplayName("Maximum Salary")]
        [DataType(DataType.Currency)]
        [Range(0, 999999999999999999.99, ErrorMessage = "Maximum Salary range from {2} to {1} dolars.")]
        public decimal SalaryMax { get; set; }
        [Required(ErrorMessage = "Percent is required")]
        [Range(0, 100, ErrorMessage = "Percentage range from {2} to {1} percent.")]
        public double Percent { get; set; }
        /*[Required(ErrorMessage = "Tax Status is required")]
        [DisplayName("Tax Status")]
        public TaxEnum.TaxStatus TaxStatus { get; set; }*/
    }
}
