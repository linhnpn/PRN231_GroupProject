using AutoMapper;
using GroupProject_HRM_Library.DTOs.Employee;
using GroupProject_HRM_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile() {
            CreateMap<Employee, GetEmployeeResponse>()
                .ForMember(dept => dept.StartDateEmployeeProject, opt => opt.MapFrom(src => src.EmployeeProjects.First().StartDate))
                .ForMember(dept => dept.EndDateEmployeeProject, opt => opt.MapFrom(src => src.EmployeeProjects.First().EndDate))
                .ForMember(dept => dept.StatusEmployeeProject, opt => opt.MapFrom(src => src.EmployeeProjects.First().EmployeeProjectStatus))
                .ForMember(dept => dept.IdOfProjectCurrent, opt => opt.MapFrom(src => src.EmployeeProjects.First().ProjectID))
                .ReverseMap();
            CreateMap<Employee, GetListEmployeeResponseIDandName>().ReverseMap();
            CreateMap<Employee, GetProfileResponse>().ForMember(dept => dept.GetEmployeeProjectResponse, opt => opt.MapFrom(src => src.EmployeeProjects.First()))
                .ReverseMap();
        }
    }
}
