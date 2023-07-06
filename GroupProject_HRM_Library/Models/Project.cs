using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GroupProject_HRM_Library.Models
{
    public class Project
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectID { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
        public int ProjectStatus { get; set; }
        public decimal ProjectBonus { get; set; } = 0;
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<EmployeeProject>? EmployeeProjects { get; set; }
    }
}