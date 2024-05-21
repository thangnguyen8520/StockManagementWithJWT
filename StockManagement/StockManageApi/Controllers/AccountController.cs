using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StockManageBusinessObjects.Models;
using StockManageDAO;
using StockManageService;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static StockManageDAO.AccountDAO;

namespace StockManageApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService iAccountService;
        private readonly IConfiguration _configuration;
        private readonly ITokenService iTokenService;

        public AccountController(ITokenService tokenService, IAccountService accountService, IConfiguration configuration)
        {
            iTokenService = tokenService;
            iAccountService = accountService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Get([FromBody] AccountDTO accountDTO)
        {
            var account = iAccountService.GetAccount(accountDTO.Email, accountDTO.Password);
            if (account != null)
            {
                var token = iTokenService.GenerateJwtToken(accountDTO.Email, _configuration);
                var refreshToken = iTokenService.GenerateRefreshToken();
                account.RefreshToken = refreshToken;
                iAccountService.UpdateAccount(account);
                return Ok(new { Token = token, RefreshToken = refreshToken });
            }
            return Unauthorized(); // Return 401 if authentication fails
        }

        [HttpPost("refresh-token")]
        public IActionResult Refresh([FromBody] TokenRequest tokenRequest)
        {
            if (tokenRequest is null)
            {
                return BadRequest("Invalid client request");
            }

            string accessToken = tokenRequest.AccessToken;
            string refreshToken = tokenRequest.RefreshToken;

            var principal = iTokenService.GetPrincipalFromExpiredToken(accessToken, _configuration);
            var emailClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            if (emailClaim == null)
            {
                return BadRequest("Invalid client request");
            }

            var email = emailClaim.Value;

            var account = iAccountService.GetAccountByEmail(email);
            if (account == null || account.RefreshToken != refreshToken)
            {
                return BadRequest("Invalid client request");
            }

            var newAccessToken = iTokenService.GenerateJwtToken(email, _configuration);
            var newRefreshToken = iTokenService.GenerateRefreshToken();

            account.RefreshToken = newRefreshToken;
            iAccountService.UpdateAccount(account);

            return Ok(new { Token = newAccessToken, RefreshToken = newRefreshToken });
        }

        [Authorize]
        [HttpPost("revoke-token")]
        public IActionResult Revoke()
        {
            var email = User.Identity.Name;
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Invalid client request");
            }

            iTokenService.RevokeRefreshToken(email);
            return NoContent();
        }

        [Authorize]
        [HttpPost("hello")]
        public IActionResult Post([FromBody] AccountDTO accountDTO)
        {
            return Ok(iAccountService.GetAccount(accountDTO.Email, accountDTO.Password));
        }
    }
}
