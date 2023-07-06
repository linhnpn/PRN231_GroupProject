using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GroupProject_HRM_Library.Models
{
    public class Notification
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationID { get; set; }
        public string NotificationDetail { get; set; }
        public DateTime Timestamp { get; set; }
        public bool isRead { get; set; }
        public int EmployeeID { get; set; }
        public Employee? Employee { get; set; }
    }
}