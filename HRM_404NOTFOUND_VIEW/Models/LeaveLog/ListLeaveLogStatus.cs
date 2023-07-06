namespace GroupProject_HRM_View.Models.LeaveLog
{
    public static class ListLeaveLogStatus
    {
        public static List<LeaveLogStatus> Values { get; }

        static ListLeaveLogStatus()
        {
            Values = new List<LeaveLogStatus> { 
                new LeaveLogStatus(0, "Waiting"),
                new LeaveLogStatus(1, "Accept"),
                new LeaveLogStatus(-1, "Reject"),
                new LeaveLogStatus(-2, "Unauthorized leave"),
                new LeaveLogStatus(2, "Cancel"),
            };
        }
    }

    public static class ListOvertimeStatus
    {
        public static List<LeaveLogStatus> Values { get; }

        static ListOvertimeStatus()
        {
            Values = new List<LeaveLogStatus> {
                new LeaveLogStatus(0, "Waiting"),
                new LeaveLogStatus(1, "Done"),
                new LeaveLogStatus(-1, "Cancel")
            };
        }
    }

    public static class ListEmployeeProjectStatus
    {
        public static List<LeaveLogStatus> Values { get; }

        static ListEmployeeProjectStatus()
        {
            Values = new List<LeaveLogStatus> {
                new LeaveLogStatus(0, "NotStart"),
                new LeaveLogStatus(1, "Work In Progress"),
                new LeaveLogStatus(-1, "Not Start")
            };
        }
    }
}
