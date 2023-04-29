using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.BAL.DTOs.ResponseDtos
{
    public class LoginResponseDto
    {
        public string JwtToken { get; set; } = string.Empty;
        public string Message { get; set; }
    }
}
