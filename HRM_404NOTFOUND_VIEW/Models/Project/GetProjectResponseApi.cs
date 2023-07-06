using GroupProject_HRM_Library.DTOs.Project;

namespace GroupProject_HRM_View.Models.Project
{
    public class GetProjectResponseApi
    {
        public bool Success { get; set; }
        public GetProjectDetailResponse? Data { get; set; }
    }
}
