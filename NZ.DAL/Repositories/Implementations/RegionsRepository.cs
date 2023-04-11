using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NZWalks.BAL.RepositoryInterfaces;
using NZWalks.DAL.Context;
using NZWalks.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.DAL.Repositories.Implementations
{
    public class RegionsRepository : IRegionsRespository
    {
        private readonly NZWalksDbContext _dbContext;

        public RegionsRepository(NZWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async void BeginTransactionAsync()
        {
            await _dbContext.Database.BeginTransactionAsync();
        }

        public async void CommitTransactionAsync()
        {
            await _dbContext.Database.CommitTransactionAsync();
        }

        public async void RollbackTransactionAsync()
        {
            await _dbContext.Database.RollbackTransactionAsync();
        }

        public async Task<Guid> CreateRegionAsync(Region newRegion)
        {
            var regionObj = await _dbContext.Regions.AddAsync(newRegion);
            await _dbContext.SaveChangesAsync();

            return regionObj.Entity.Id;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetRegionByIdAsync(Guid id)
        {
            return await _dbContext.Regions.FindAsync(id);
        }

        public async Task<Region?> UpdateRegionAsync(Guid id, Region region)
        {
            var currentRegion = await GetRegionByIdAsync(id);
            if (currentRegion != null)
            {
                currentRegion.Code = region.Code;
                currentRegion.Name = region.Name;
                currentRegion.RegionImageUrl = region.RegionImageUrl;
            }
            
            return (await _dbContext.SaveChangesAsync()) == 1 ? currentRegion : null;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var currentRegion = await GetRegionByIdAsync(id);
            var deletedEntity = new Region();
            if (currentRegion != null)
            {
                deletedEntity = _dbContext.Regions.Remove(currentRegion).Entity;
                await SaveChangesAsync();
                return deletedEntity;
            }
            
            return null;
        }
    }
}
