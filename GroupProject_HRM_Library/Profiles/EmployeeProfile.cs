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
            CreateMap<Employee, GetEmployeeResponse>().ReverseMap();
            CreateMap<Employee, GetProfileResponse>().ForMember(dept => dept.GetEmployeeProjectResponse, opt => opt.MapFrom(src => src.EmployeeProjects.First()))
                .ReverseMap();
        }
    }
}
