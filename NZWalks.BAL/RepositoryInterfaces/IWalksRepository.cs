using NZWalks.Domain.Models;

namespace NZWalks.BAL.RepositoryInterfaces
{
    public interface IWalksRepository
    {
        Task<Walk?> CreateAsync(Walk walk);
        Task<Walk?> DeleteAsync(Guid id);
        Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 0, int pageSize = 1000);
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk?> UpdateWalk(Guid id, Walk updateWalkRequest);
    }
}
