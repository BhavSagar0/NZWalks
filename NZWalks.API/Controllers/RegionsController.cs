using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.DAL.Context;
using NZWalks.Domain.Models;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;

        public RegionsController(NZWalksDbContext nZWalksDbContext)
        {
            _dbContext = nZWalksDbContext;
        }

        //Get all Regions
        [HttpGet]
        public IActionResult GetAll() 
        {
            var regions = _dbContext.Regions.ToList();

            return Ok(regions);
        }

        //Get Single Region (Get Region by Id)
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id) 
        {
            //var region = _dbContext.Regions.Find(id);
            var region = _dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (region == null)
            {
                return NotFound();
            }

            return Ok(region);
        }
    }
}
