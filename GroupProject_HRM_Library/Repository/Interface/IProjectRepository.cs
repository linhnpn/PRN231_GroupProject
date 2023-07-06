using GroupProject_HRM_Library.DTOs.Project;
using GroupProject_HRM_Library.Enums;
using GroupProject_HRM_Library.Models;

namespace GroupProject_HRM_Library.Repository.Interface
{
    public interface IProjectRepository
    {
        public Task CreateProjectRequestAsync(CreateProjectRequest request);
        public Task UpdateProjectRequestAsync(int id, UpdateProjectRequest request);
        public Task UpdateProjectStatusRequestAsync(int id, ProjectEnum.ProjectStatus status);
        public Task<int> GetIdProjectBaseOnManager(int id);
        public Task<GetProjectDetailResponse> GetProjectResponseAsync(int id);
        public Task<List<GetProjectResponse>> GetProjectResponsesAsync();
        public Task<List<GetProjectResponse>> GetProjectResponsesSortedAsync(
            string? projectName,
            decimal? bonus = null,
            ProjectEnum.ProjectStatus? status = null,
            ProjectEnum.ProjectOrderBy? orderBy = null);
        public Task DeleteProjectRequestAsync(int id);

        public Task<List<Project>> GetAllProjectCanAssignEmployee();
    }
}
