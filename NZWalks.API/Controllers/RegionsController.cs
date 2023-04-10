using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.BAL.Contracts;
using NZWalks.BAL.DTOs;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionsService _regionsService;

        public RegionsController(IRegionsService regionsService)
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

        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var newRegion = _regionsService.CreateRegion(addRegionRequestDto);

            return CreatedAtAction(nameof(GetById), new { id = newRegion.Id }, newRegion);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var result = _regionsService.UpdateRegion(id, updateRegionRequestDto);

            if(result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var result = _regionsService.DeleteRegion(id);

            return result == null ? NotFound() : Ok(result);
        }

    }
}
