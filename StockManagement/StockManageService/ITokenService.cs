using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StockManageService
{
    public interface ITokenService
    {
        public string GenerateJwtToken(string email, IConfiguration configuration);
        public string GenerateRefreshToken();
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration configuration);
        public void RevokeRefreshToken(string email);

    }
}
