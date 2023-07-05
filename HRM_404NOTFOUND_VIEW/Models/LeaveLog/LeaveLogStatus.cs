namespace GroupProject_HRM_View.Models.LeaveLog
{
    public class LeaveLogStatus
    {
        public LeaveLogStatus(int StatusId, string StatusName) {
            this.StatusID = StatusId;
            this.StatusName = StatusName;
        }
        public int StatusID { get; set; }
        public string StatusName { get; set; }
    }
}
