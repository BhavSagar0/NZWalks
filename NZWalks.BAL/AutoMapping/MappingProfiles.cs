using AutoMapper;
using NZWalks.BAL.DTOs;
using NZWalks.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.BAL.AutoMapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
        }
    }
}
