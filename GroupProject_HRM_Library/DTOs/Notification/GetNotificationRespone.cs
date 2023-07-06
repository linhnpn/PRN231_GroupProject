using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.DTOs.Notification
{
    public class GetNotificationRespone
    {
        public string NotificationDetail { get; set; }
        public DateTime Timestamp { get; set; }
        public bool isRead { get; set; }
    }
}
