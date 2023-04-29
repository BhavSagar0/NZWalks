using Microsoft.AspNetCore.Identity;
using NZWalks.BAL.DTOs.RequestDtos;

namespace NZWalks.BAL.RepositoryInterfaces
{
    public interface IAuthRepository
    {
        public Task<IdentityResult> RegisterAsync(IdentityUser userInfo, string password);
        public Task<IdentityResult> AddUserRolesAsync(IdentityUser userInfo ,string[] roles);
        public Task<string> LoginAsync(string username, string password);
    }
}