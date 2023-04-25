using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.BAL.Contracts;
using NZWalks.BAL.DTOs;
using NZWalks.BAL.DTOs.RequestDtos;
using NZWalks.BAL.Implementations;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalksService _walksService;

        public WalksController(IWalksService walksService)
        {
            _walksService = walksService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks([FromQuery] string? filterOn, [FromQuery] string? filterQuery, 
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walks = await _walksService.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            return Ok(walks);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            var walk = await _walksService.CreateWalkAsync(addWalkRequestDto);

            if(walk == null)
                return StatusCode(500);

            return CreatedAtAction(nameof(GetWalkById), new { id = walk.Id }, walk);
        }

        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid Id)
        {
            var walk = await _walksService.GetWalkByIdAsync(Id);

            if(walk == null)
                return NotFound();

            return Ok(walk);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequest)
        {
            var walk = await _walksService.UpdateWalk(id, updateWalkRequest);

            if(walk == null)
                return StatusCode(500);

            return Ok(walk);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
            var result = await _walksService.DeleteAsync(id);

            if(result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
