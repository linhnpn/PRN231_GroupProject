using AutoMapper;
using GroupProject_HRM_Library.DTOs.Tax;
using GroupProject_HRM_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_HRM_Library.Profiles
{
    public class TaxProfile : Profile
    {
        public TaxProfile()
        {
            CreateMap<Tax, GetTaxResponse>().ReverseMap();
            CreateMap<CreateTaxRequest, Tax>().ReverseMap();
            CreateMap<UpdateTaxRequest, Tax>().ReverseMap();
        }
    }
}
