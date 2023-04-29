using Microsoft.AspNetCore.Identity;
using NZWalks.BAL.DTOs.RequestDtos;
using NZWalks.BAL.DTOs.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.BAL.Contracts
{
    public interface IAuthService
    {
        public Task<IdentityResult?> RegisterAsync(RegisterRequestDto registerRequestDto);

        public Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
    }
}
