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
        public Guid CreateRegion(Region newRegion);
        public List<Region> GetAll();
        public Region GetRegionById(Guid id);
        public void BeginTransaction();
        public void CommitTransaction();
        public void RollbackTransaction();
        public Region? UpdateRegion(Guid id, Region region);
        public int SaveChanges();
        public Region? Delete(Guid id);
    }
}
