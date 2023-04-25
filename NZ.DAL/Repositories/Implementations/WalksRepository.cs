using Microsoft.EntityFrameworkCore;
using NZWalks.BAL.RepositoryInterfaces;
using NZWalks.DAL.Context;
using NZWalks.Domain.Models;

namespace NZWalks.DAL.Repositories.Implementations
{
    public class WalksRepository : IWalksRepository
    {
        private readonly NZWalksDbContext _dbContext;

        public WalksRepository(NZWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Walk?> CreateAsync(Walk walk)
        {
            var walkObj = await _dbContext.Walks.AddAsync(walk);

            var result = await _dbContext.SaveChangesAsync();

            return result == 1 ? walkObj.Entity : null;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var walk = await _dbContext.Walks.FindAsync(id);
            var deletedEntity = new Walk();
            var result = 0;
            if (walk != null)
            {
                deletedEntity = _dbContext.Remove(walk).Entity;
                result = await _dbContext.SaveChangesAsync();
                if (result == 1)
                    return deletedEntity;
            }

            return null;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 0, int pageSize = 1000)
        {
            var walks = _dbContext.Walks.Include(x => x.Difficulty).Include(x => x.Region).AsQueryable();

            //Filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            //Sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    walks = isAscending == true ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);

                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                    walks = isAscending == true ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
            }

            //Pagination
            var skipResults = (pageNumber - 1) * pageSize;


            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
            //return await _dbContext.Walks.Include(x => x.Difficulty).Include(x => x.Region).ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Walks.Include(x => x.Region).Include(x => x.Difficulty).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateWalk(Guid id, Walk updateWalkRequest)
        {
            var walk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (walk != null)
            {
                walk.Description = updateWalkRequest.Description;
                walk.Name = updateWalkRequest.Name;
                walk.LengthInKm = updateWalkRequest.LengthInKm;
                walk.WalkImageUrl = updateWalkRequest.WalkImageUrl;
                walk.DifficultyId = updateWalkRequest.DifficultyId;
                walk.RegionId = updateWalkRequest.RegionId;
            }

            var result = await _dbContext.SaveChangesAsync();

            return result == 1 ? walk : null;
        }
    }
}
