using NZWalks.BAL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.BAL.Contracts
{
    public interface IRegionsService
    {
        public List<RegionDto> GetAll();
        public RegionDto GetRegionById(Guid id);
    }
}
