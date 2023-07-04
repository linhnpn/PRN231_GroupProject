using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DTOs.Bonus
{
    public class BonusRequest
    {
        [Required(ErrorMessage = "Employee ID is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Bonus Value is required")]
        [DataType(DataType.Currency)]
        [Range(0, 999999999999999.99)]
        public decimal BonusValue { get; set; }

        [Required(ErrorMessage = "Timestamp is required")]
        public DateTime Timestamp { get; set; }

        [Required(ErrorMessage = "Employee ID is required")]
        public int EmployeeID { get; set; }
    }
}
