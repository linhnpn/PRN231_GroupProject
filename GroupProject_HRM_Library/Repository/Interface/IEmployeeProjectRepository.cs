using GroupProject_HRM_Library.DTOs.EmployeeProject;
using GroupProject_HRM_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.Repository.Interface
{
    public interface IEmployeeProjectRepository
    {
        public Task<EmployeeProject> AssignEmployeeToProject(AssignEmployeeToProjectRequest assignRequest);
    }
}
