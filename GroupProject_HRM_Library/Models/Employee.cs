using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GroupProject_HRM_Library.Models
{
    public class Employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeID { get; set; }
        public string? EmployeeName { get; set; }
        public string? EmployeeImage { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime JoinDate { get; set; } = DateTime.Now;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int EmployeeStatus { get; set; }
        public int RoleID { get; set; }
        [JsonIgnore]
        public Role? Role { get; set; }
        [JsonIgnore]
        public virtual ICollection<Income>? Incomes { get; set;}
        [JsonIgnore]
        public virtual ICollection<Notification>? Notifications { get; set; }
        [JsonIgnore]
        public virtual ICollection<EmployeeProject>? EmployeeProjects { get; set;}
        [JsonIgnore]
        public virtual ICollection<Payroll>? Payrolls { get; set;}
        [JsonIgnore]
        public virtual ICollection<LeaveLog>? LeaveLogs { get; set; }
        [JsonIgnore]
        public virtual ICollection<OvertimeLog>? OvertimeLogs { get; set; }
        [JsonIgnore]
        public virtual ICollection<Bonus>? Bonuses { get; set; }

    }
}
