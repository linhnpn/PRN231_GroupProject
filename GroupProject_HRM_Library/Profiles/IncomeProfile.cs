using AutoMapper;
using GroupProject_HRM_Library.DTOs.Income;
using GroupProject_HRM_Library.Models;

namespace GroupProject_HRM_Library.Profiles
{
    public class IncomeProfile : Profile
    {
        public IncomeProfile()
        {
            CreateMap<Income, GetIncomeEmployeeResponse>().ReverseMap();
            CreateMap<Income, CreateIncomeEmployeeResponse>().ReverseMap();
        }
    }
}