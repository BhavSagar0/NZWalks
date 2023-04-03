using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.BAL.Contracts;
using NZWalks.DAL.Context;
using NZWalks.Domain.Models;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionsService _regionsService;

        public RegionsController(NZWalksDbContext nZWalksDbContext, IRegionsService regionsService)
        {
            _regionsService = regionsService;
        }

        //Get all Regions
        [HttpGet]
        public IActionResult GetAll() 
        {
            var regions = _regionsService.GetAll();

            return Ok(regions);
        }

        //Get Single Region (Get Region by Id)
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id) 
        {
            //var region = _dbContext.Regions.Find(id);
            var region = _regionsService.GetRegionById(id);

            if (region == null)
            {
                return NotFound();
            }

            return Ok(region);
        }
    }
}
