using NZWalks.Domain.Models;

namespace NZWalks.BAL.RepositoryInterfaces
{
    public interface IWalksRepository
    {
        Task<Walk?> CreateAsync(Walk walk);
        Task<Walk?> DeleteAsync(Guid id);
        Task<List<Walk>> GetAllAsync();
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk?> UpdateWalk(Guid id, Walk updateWalkRequest);
    }
}
