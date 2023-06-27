using AutoMapper;
using GroupProject_HRM_Library.DTOs.Employee;
using GroupProject_HRM_Library.DTOs.Project;
using GroupProject_HRM_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile() {
            CreateMap<Project, GetProjectResponse>().ReverseMap();
        }
    }
}
