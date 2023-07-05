using AutoMapper;
using GroupProject_HRM_Library.DTOs.Payroll;
using GroupProject_HRM_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.Profiles
{
    public class PayrollProfile : Profile
    {
        public PayrollProfile() {
            CreateMap<Payroll, PayrollRequest>().ReverseMap();
            CreateMap<Payroll, GetPayrollResponse>().ReverseMap();
        }
    }
}
