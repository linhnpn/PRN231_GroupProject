﻿namespace GroupProject_HRM_View.Models.Employee
{
    public class GetAllEmployeeReponse
    {
        public bool Success { get; set; }
        public List<GroupProject_HRM_Library.Models.Employee> Data { get; set; }
    }
}
