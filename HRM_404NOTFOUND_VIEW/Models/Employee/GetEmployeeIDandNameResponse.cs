using GroupProject_HRM_Library.DTOs.Employee;

namespace GroupProject_HRM_View.Models.Employee
{
    public class GetEmployeeIDandNameResponse
    {
        public bool Success { get; set; }
        public List<GetListEmployeeResponseIDandName> Data { get; set; }
    }
}
