﻿using AutoMapper;
using GroupProject_HRM_Library.DTOs.Income;
using GroupProject_HRM_Library.DTOs.LeaveLog;
using GroupProject_HRM_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.Profiles
{
    public class IncomeProfile : Profile
    {
        public IncomeProfile() {
            CreateMap<Income, GetIncomeEmployeeResponse>().ReverseMap();
        }
    }
}
