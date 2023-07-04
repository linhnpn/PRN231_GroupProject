using GroupProject_HRM_Library.DTOs.Authenticate;
using GroupProject_HRM_Library.DTOs.Employee;

namespace GroupProject_HRM_View.Models.Employee
{
    public class GetProfileResponseApi
    {
        public bool Success { get; set; }
        public GetProfileResponse Data { get; set; }
    }
}
