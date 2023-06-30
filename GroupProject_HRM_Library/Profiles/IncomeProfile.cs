using AutoMapper;
using GroupProject_HRM_Library.DTOs.Income;
using GroupProject_HRM_Library.DTOs.LeaveLog;
using GroupProject_HRM_Library.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace GroupProject_HRM_Library.Profiles
    public class IncomeProfile : Profile
{
    {
        public IncomeProfile() {
            CreateMap<Income, GetIncomeEmployeeResponse>().ReverseMap();
            CreateMap<Income, CreateIncomeEmployeeResponse>().ReverseMap();
        }
    }
}