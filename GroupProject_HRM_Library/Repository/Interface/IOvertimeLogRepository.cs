using GroupProject_HRM_Library.DTOs.OvertimeLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.Repository.Interface
{
    public interface IOvertimeLogRepository
    {
        public Task CreateOvertimeLogRequestAsync(OvertimeLogRequest request);
        public Task UpdateOvertimeLogRequestAsync(int id, UpdateOvertimeLogRequest request);
        public Task UpdateStatusRequestAsync(int id, int status);
        public Task<GetOvertimeLogResponse> GetOvertimeLogAsync(int id);
        public Task<List<GetOvertimeLogResponse>> GetOvertimeLogResponsesByStatusAsync(int? status);
        public Task<List<GetOvertimeLogResponse>> GetOvertimeLogResponsesByEmplIDAsync(int emplID, int? status);
        public Task<List<GetOvertimeLogResponse>> GetOvertimeLogResponsesByProjectIDAsync(int projectID, int? status);
    }
}
