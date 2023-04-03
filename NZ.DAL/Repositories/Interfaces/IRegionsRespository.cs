using NZWalks.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.DAL.Repositories.Interfaces
{
    public interface IRegionsRespository
    {
        public List<Region> GetAll();
        public Region GetRegionById(Guid id);
    }
}
