﻿using NZWalks.BAL.DTOs;
using NZWalks.BAL.DTOs.RequestDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.BAL.Contracts
{
    public interface IWalksService
    {
        Task<WalkDto?> CreateWalkAsync(AddWalkRequestDto addWalkRequestDto);
        Task<WalkDto?> DeleteAsync(Guid id);
        Task<List<WalkDto>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 0, int pageSize = 1000);
        Task<WalkDto> GetWalkByIdAsync(Guid id);
        Task<WalkDto?> UpdateWalk(Guid id, UpdateWalkRequestDto updateWalkRequest);
    }
}
