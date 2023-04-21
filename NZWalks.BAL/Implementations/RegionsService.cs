using AutoMapper;
using NZWalks.BAL.Contracts;
using NZWalks.BAL.DTOs;
using NZWalks.BAL.DTOs.RequestDtos;
using NZWalks.BAL.RepositoryInterfaces;
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

        public async Task<RegionDto?> CreateRegionAsync(AddRegionRequestDto addRegionRequestDto)
        {
            var newRegion = new Region();
            var regionDto = new RegionDto();
            try
            {
                newRegion = _mapper.Map<AddRegionRequestDto, Region>(addRegionRequestDto);

                var newRegionId = await _regionsRepository.CreateRegionAsync(newRegion);

                regionDto = await GetRegionByIdAsync(newRegionId);
            }
            catch (Exception ex)
            {
            }
            return regionDto;
        }

        public async Task<List<RegionDto>> GetAllAsync()
        {
            var regions = new List<RegionDto>();
            try 
            {
                regions = _mapper.Map<List<Region>, List<RegionDto>>(await _regionsRepository.GetAllAsync());
            }
            catch (Exception ex) 
            {
            }
            return regions;
        }

        public async Task<RegionDto?> GetRegionByIdAsync(Guid id)
        {
            var region = new RegionDto();
            try
            {
                region = _mapper.Map<RegionDto>(await _regionsRepository.GetRegionByIdAsync(id));
            }
            catch (Exception ex)
            {
            }
            return region;
        }

        public async Task<RegionDto?> UpdateRegionAsync(Guid id, UpdateRegionRequestDto updateRegionRequestDto)
        {
            var result = new Region();
            try {
                    result = await _regionsRepository.UpdateRegionAsync(id, _mapper.Map<Region>(updateRegionRequestDto));
            }
            catch (Exception ex) {
            }
            return _mapper.Map<RegionDto>(result);
        }

        public async Task<RegionDto?> DeleteRegionAsync(Guid id)
        {
            Region? result = null;
            try
            {
                var region = await GetRegionByIdAsync(id);

                if (region == null)
                    return null;

                result = await _regionsRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
            }
            return _mapper.Map<RegionDto>(result);
        }
    }
}
