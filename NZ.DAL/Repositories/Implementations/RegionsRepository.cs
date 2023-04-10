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

        public void BeginTransaction()
        {
            _dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _dbContext.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _dbContext.Database.RollbackTransaction();
        }

        public Guid CreateRegion(Region newRegion)
        {
            var regionObj = _dbContext.Regions.Add(newRegion);
            _dbContext.SaveChanges();

            return regionObj.Entity.Id;
        }

        public List<Region> GetAll()
        {
            return _dbContext.Regions.ToList();
        }

        public Region? GetRegionById(Guid id)
        {
            return _dbContext.Regions.Find(id);
        }

        public Region? UpdateRegion(Guid id, Region region)
        {
            var currentRegion = GetRegionById(id);
            if (currentRegion != null)
            {
                currentRegion.Code = region.Code;
                currentRegion.Name = region.Name;
                currentRegion.RegionImageUrl = region.RegionImageUrl;
            }
            
            return _dbContext.SaveChanges() == 1 ? currentRegion : null;
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public Region? Delete(Guid id)
        {
            var currentRegion = GetRegionById(id);
            var deletedEntity = new Region();
            if (currentRegion != null)
            {
                deletedEntity = _dbContext.Regions.Remove(currentRegion).Entity;
                SaveChanges();
                return deletedEntity;
            }
            
            return null;
        }
    }
}
