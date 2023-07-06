using System.ComponentModel.DataAnnotations;

namespace GroupProject_HRM_Library.DTOs.LeaveLog
{
    public class UpdateLeaveLogRequest
    {
        [Required]
        public int LeaveLogID { get; set; }
        [Required]
        public string? RejectReson { get; set; }
    }
}
