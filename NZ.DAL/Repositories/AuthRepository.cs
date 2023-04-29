using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NZWalks.BAL.DTOs.RequestDtos;
using NZWalks.BAL.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.DAL.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthRepository(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> AddUserRolesAsync(IdentityUser userInfo, string[] roles)
        {
            var identityResult = await _userManager.AddToRolesAsync(userInfo, roles);

            return identityResult;
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var user = await _userManager.FindByEmailAsync(username);

            if (user != null)
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, password);

                if (checkPasswordResult)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        var jwtToken = await CreateJWTToken(user, roles);
                        return jwtToken;
                    }
                }
            }

            return string.Empty;
        }

        public async Task<IdentityResult> RegisterAsync(IdentityUser userInfo, string password)
        {
            var identityResult = await _userManager.CreateAsync(userInfo, password);

            return identityResult;
        }

        //Helper Method
        private async Task<string> CreateJWTToken(IdentityUser user, IEnumerable<string> roles)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
