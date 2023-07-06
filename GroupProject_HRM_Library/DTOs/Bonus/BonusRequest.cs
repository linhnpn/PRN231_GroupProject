using System.ComponentModel.DataAnnotations;

namespace GroupProject_HRM_Library.DTOs.Bonus
{
    public class BonusRequest
    {
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Bonus Value is required")]
        [DataType(DataType.Currency)]
        [Range(0, 999999999999999.99)]
        public decimal BonusValue { get; set; }

        [Required(ErrorMessage = "Timestamp is required")]
        [DataType(DataType.DateTime)]
        public DateTime Timestamp { get; set; }
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "EmployeeID is invalid")]
        [Range(1, 3)]
        [Required(ErrorMessage = "Employee ID is required")]
        public int EmployeeID { get; set; }
    }
}
