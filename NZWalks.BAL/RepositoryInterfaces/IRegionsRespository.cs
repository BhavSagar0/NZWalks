using NZWalks.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.BAL.RepositoryInterfaces
{
    public interface IRegionsRespository
    {
        public Task<Guid> CreateRegionAsync(Region newRegion);
        public Task<List<Region>> GetAllAsync();
        public Task<Region?> GetRegionByIdAsync(Guid id);
        public Task<Region?> UpdateRegionAsync(Guid id, Region region);
        public Task<int> SaveChangesAsync();
        public Task<Region?> DeleteAsync(Guid id);
    }
}
