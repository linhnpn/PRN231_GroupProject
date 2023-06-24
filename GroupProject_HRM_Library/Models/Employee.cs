using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace GroupProject_HRM_Library.Models
{
    public class Employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeImage { get; set; }
        public int Address { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime BirthDate { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int EmployeeStatus { get; set; }
        public int RoleID { get; set; }
        public Role Role { get; set; }
        public virtual ICollection<Income> Incomes { get; set;}
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set;}
        public virtual ICollection<Payroll> Payrolls { get; set;}
        public virtual ICollection<LeaveLog> LeaveLogs { get; set; }
        public virtual ICollection<OvertimeLog> OvertimeLogs { get; set; }
        public virtual ICollection<Bonus> Bonuses { get; set; }

    }
}
