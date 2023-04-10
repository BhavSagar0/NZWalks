using AutoMapper;
using NZWalks.BAL.Contracts;
using NZWalks.BAL.DTOs;
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

        public RegionDto CreateRegion(AddRegionRequestDto addRegionRequestDto)
        {
            var newRegion = new Region();
            var regionDto = new RegionDto();
            _regionsRepository.BeginTransaction();
            try
            {
                newRegion = _mapper.Map<AddRegionRequestDto, Region>(addRegionRequestDto);

                var newRegionId = _regionsRepository.CreateRegion(newRegion);
                _regionsRepository.CommitTransaction();

                regionDto = GetRegionById(newRegionId);
            }
            catch (Exception ex)
            {
                _regionsRepository.RollbackTransaction();
            }
            return regionDto;
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

        public RegionDto? UpdateRegion(Guid id, UpdateRegionRequestDto updateRegionRequestDto)
        {
            _regionsRepository.BeginTransaction();
            var result = new Region();
            try {
                    result = _regionsRepository.UpdateRegion(id, _mapper.Map<Region>(updateRegionRequestDto));

                    if (result != null)
                        _regionsRepository.CommitTransaction();
                    
                    else
                        _regionsRepository.RollbackTransaction();
            }
            catch (Exception ex) {
                _regionsRepository.RollbackTransaction();
            }
            return _mapper.Map<RegionDto>(result);
        }

        public RegionDto? DeleteRegion(Guid id)
        {
            Region? result = null;
            try
            {
                var region = GetRegionById(id);

                if (region == null)
                    return null;

                _regionsRepository.BeginTransaction();
                result = _regionsRepository.Delete(id);
                if (result == null)
                    _regionsRepository.RollbackTransaction();
                else
                    _regionsRepository.CommitTransaction(); 
            }
            catch (Exception ex)
            {
                _regionsRepository.RollbackTransaction();
            }
            return _mapper.Map<RegionDto>(result);
        }
    }
}
