using AutoMapper;
using NZWalks.BAL.Contracts;
using NZWalks.BAL.DTOs;
using NZWalks.DAL.Repositories.Interfaces;
using NZWalks.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.BAL.Implementations
{
    public class RegionsService : IRegionsService
    {
        private readonly IRegionsRespository _regionsRepository;
        private readonly IMapper _mapper;

        public RegionsService(IRegionsRespository regionsRepository, IMapper mapper) 
        {
            _regionsRepository = regionsRepository;
            _mapper = mapper;
        }
        public List<RegionDto> GetAll()
        {
            var regions = new List<RegionDto>();
            try 
            {
                regions = _mapper.Map<List<Region>, List<RegionDto>>(_regionsRepository.GetAll());
            }
            catch (Exception ex) 
            {
            }
            return regions;
        }

        public RegionDto GetRegionById(Guid id)
        {
            var region = new RegionDto();
            try
            {
                region = _mapper.Map<Region, RegionDto>(_regionsRepository.GetRegionById(id));
            }
            catch (Exception ex)
            {
            }
            return region;
        }
    }
}
