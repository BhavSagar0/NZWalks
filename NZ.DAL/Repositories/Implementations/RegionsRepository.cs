using NZWalks.DAL.Context;
using NZWalks.DAL.Repositories.Interfaces;
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

        public List<Region> GetAll()
        {
            return _dbContext.Regions.ToList();
        }

        public Region? GetRegionById(Guid id)
        {
            return _dbContext.Regions.Find(id);
        }
    }
}
