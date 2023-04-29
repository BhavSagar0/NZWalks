using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.BAL.Contracts;
using NZWalks.BAL.DTOs.RequestDtos;

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
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll() 
        {
            var regions = await _regionsService.GetAllAsync();

            return Ok(regions);
        }

        //Get Single Region (Get Region by Id)
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
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
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            if (ModelState.IsValid)
            {
                var newRegion = await _regionsService.CreateRegionAsync(addRegionRequestDto);

                return CreatedAtAction(nameof(GetById), new { id = newRegion.Id }, newRegion);
            }
            else
                return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var result = await _regionsService.UpdateRegionAsync(id, updateRegionRequestDto);

            if(result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer, Reader")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _regionsService.DeleteRegionAsync(id);

            return result == null ? NotFound() : Ok(result);
        }

    }
}
