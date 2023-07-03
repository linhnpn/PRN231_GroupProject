using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

#nullable disable

namespace FUFlowerBouquetManagementLibrary.Models
{
    public partial class FlowerBouquet
    {
        [Required(ErrorMessage = "Flower Bouquet Id is required")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FlowerBouquetId { get; set; }
        [Required(ErrorMessage = "Category Id is required")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Flower Bouquet Name is required")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Name is invalid")]
        public string FlowerBouquetName { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "UnitPrice is required")]
        [RegularExpression(@"^[1-9]\d*(\.\d+)?$", ErrorMessage = "UnitPrice is invalid")]
        public decimal UnitPrice { get; set; }
        [Required(ErrorMessage = "UnitsInStock is required")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "UnitsInStock is invalid")]
        public int UnitsInStock { get; set; }
        public byte? FlowerBouquetStatus { get; set; }
        [Required]
        public int? SupplierId { get; set; }
    }
}
