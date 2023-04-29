using Microsoft.AspNetCore.Identity;
using NZWalks.BAL.Contracts;
using NZWalks.BAL.DTOs.RequestDtos;
using NZWalks.BAL.DTOs.ResponseDtos;
using NZWalks.BAL.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.BAL.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            LoginResponseDto loginResponse = new LoginResponseDto();
            try 
            {
                loginResponse.JwtToken = await _authRepository.LoginAsync(loginRequestDto.Username, loginRequestDto.Password);

                if (loginResponse.JwtToken == string.Empty)
                    loginResponse.Message = "Invalid credentials!";
                else
                    loginResponse.Message = "Log in successful!";
            }
            catch (Exception ex) { }
            return loginResponse;
        }

        public async Task<IdentityResult?> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser 
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };

            try 
            {
                var result = await _authRepository.RegisterAsync(identityUser, registerRequestDto.Password);

                if (result.Succeeded)
                {
                    result = await _authRepository.AddUserRolesAsync(identityUser, registerRequestDto.Roles);

                    if (result.Succeeded)
                        return result;
                }
            }
            catch (Exception ex) { }
            return null;
        }
    }
}
