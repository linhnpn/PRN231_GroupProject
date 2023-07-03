using AutoMapper;
using GroupProject_HRM_Library.DTOs.EmployeeProject;
using GroupProject_HRM_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.Profiles
{
    public class EmployeeProjectProfile : Profile
    {
        public EmployeeProjectProfile()
        {
            CreateMap<EmployeeProject,GetEmployeeProjectResponse>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.EmployeeName))
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.Employee.EmailAddress))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Employee.Role.RoleName))
                .ReverseMap();
        }
    }
}
