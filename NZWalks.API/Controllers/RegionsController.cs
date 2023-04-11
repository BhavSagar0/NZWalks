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
        public async Task<IActionResult> GetAll() 
        {
            var regions = await _regionsService.GetAllAsync();

            return Ok(regions);
        }

        //Get Single Region (Get Region by Id)
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) 
        {
            //var region = _dbContext.Regions.Find(id);
            var region = await _regionsService.GetRegionByIdAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            return Ok(region);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var newRegion = await _regionsService.CreateRegionAsync(addRegionRequestDto);

            return CreatedAtAction(nameof(GetById), new { id = newRegion.Id }, newRegion);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var result = await _regionsService.UpdateRegionAsync(id, updateRegionRequestDto);

            if(result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var result = await _regionsService.DeleteRegionAsync(id);

            return result == null ? NotFound() : Ok(result);
        }

    }
}
