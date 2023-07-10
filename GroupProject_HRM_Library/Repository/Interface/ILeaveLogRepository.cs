using GroupProject_HRM_Library.DTOs.LeaveLog;

namespace GroupProject_HRM_Library.Repository.Interface
{
    public interface ILeaveLogRepository
    {
        public Task CreateLeaveLogRequestAsync(LeaveLogRequest request);
        public Task UpdateLeaveLogRequestAsync(int id, UpdateLeaveLogRequest request);
        public Task UpdateStatusRequestAsync(int id, int status);
        public Task<GetLeaveLogResponse> GetLeaveLogAsync(int id);
        public Task<List<GetLeaveLogResponse>> GetLeaveLogResponsesByStatusAsync(int? status);
        public Task<List<GetLeaveLogResponse>> GetLeaveLogResponsesByEmplIDAsync(int emplID, int? status);
        public Task<List<GetLeaveLogResponse>> GetLeaveLogResponsesByProjectIDAsync(int projectID, int? status);
        public Task CreateLeaveLogRequestAsync(LeaveLogManagerRequest request);
    }
}
