using AutoMapper;
using GroupProject_HRM_Library.DTOs.Employee;
using GroupProject_HRM_Library.DTOs.OvertimeLog;
using GroupProject_HRM_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.Profiles
{
    public class OvertimeLogProfile : Profile
    {
        public OvertimeLogProfile()
        {
            CreateMap<OvertimeLog, OvertimeLogRequest>().ReverseMap();
            CreateMap<OvertimeLog, GetOvertimeLogResponse>().ReverseMap();
            CreateMap<OvertimeLog, UpdateOvertimeLogRequest>().ReverseMap();
        }
    }
}
