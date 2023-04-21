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
    public class WalksService : IWalksService
    {
        private readonly IWalksRepository _walksRepository;
        private readonly IMapper _mapper;

        public WalksService(IWalksRepository walksRepository, IMapper mapper)
        {
            _walksRepository = walksRepository;
            _mapper = mapper;
        }
        public async Task<WalkDto?> CreateWalkAsync(AddWalkRequestDto addWalkRequestDto)
        {
            var result = new WalkDto();
            try {
                var walk = await _walksRepository.CreateAsync(_mapper.Map<Walk>(addWalkRequestDto));

                result = _mapper.Map<WalkDto?>(walk);
            }
            catch (Exception ex) {
            }
            return result;
        }

        public async Task<WalkDto?> DeleteAsync(Guid id)
        {
            var result = new WalkDto();

            try 
            {
                var deletedWalk = await _walksRepository.DeleteAsync(id);

                result = _mapper.Map<WalkDto>(deletedWalk);
            }
            catch (Exception ex) { }    
            return result;
        }

        public async Task<List<WalkDto>> GetAllAsync()
        {
            var result = new List<WalkDto>();

            try
            {
                var walks = await _walksRepository.GetAllAsync();

                result = _mapper.Map<List<WalkDto>>(walks);
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        public async Task<WalkDto> GetWalkByIdAsync(Guid id)
        {
            var result = new WalkDto();

            try
            {
                var walk = await _walksRepository.GetByIdAsync(id);

                result = _mapper.Map<WalkDto>(walk);
            }
            catch (Exception ex)
            { }

            return result;  
        }

        public async Task<WalkDto?> UpdateWalk(Guid id, UpdateWalkRequestDto updateWalkRequest)
        {
            var result = new WalkDto();

            try 
            {
                var updatedWalk = await _walksRepository.UpdateWalk(id, _mapper.Map<Walk>(updateWalkRequest));

                result = _mapper.Map<WalkDto>(updatedWalk);
            }
            catch (Exception ex) { }
            return result;
        }
    }
}
