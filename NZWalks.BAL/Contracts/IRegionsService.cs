using NZWalks.BAL.DTOs;
using NZWalks.BAL.DTOs.RequestDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.BAL.Contracts
{
    public interface IRegionsService
    {
        public Task<RegionDto?> GetRegionByIdAsync(Guid id);
        public Task<RegionDto?> CreateRegionAsync(AddRegionRequestDto addRegionRequestDto);
        public Task<RegionDto?> UpdateRegionAsync(Guid id, UpdateRegionRequestDto updateRegionRequestDto);
        public Task<RegionDto?> DeleteRegionAsync(Guid id);
        public Task<List<RegionDto>> GetAllAsync();
    }
}
