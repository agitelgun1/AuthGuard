using System;
using System.Security.Claims;
using AuthGuard.API.Contracts;
using AuthGuard.API.Models;
using AuthGuard.API.Models.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AuthGuard.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IUserService _userService;
        private readonly IAuthManager _authManager;
        private readonly TokenConfig _tokenConfig;

        public AuthenticationController(ILogger<AuthenticationController> logger, IUserService userService,
            IAuthManager authManager, IOptions<TokenConfig> tokenConfig)
        {
            _logger = logger;
            _userService = userService;
            _authManager = authManager;
            _tokenConfig = tokenConfig.Value;
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public IActionResult Token([FromBody] TokenRequest token)
        {
            if (!_userService.IsValidUserCredentials(token))
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, token.UserName)
            };

            var jwtResult = _authManager.GenerateTokens(claims);

            _logger.LogInformation($"User [{token.UserName}] logged in the system.");

            return Ok(new TokenResult
            {
                UserName = token.UserName,
                AccessToken = jwtResult?.AccessToken,
                ExpiresIn = TimeSpan.FromMinutes(_tokenConfig.AccessTokenExpiration).TotalMilliseconds
            });
        }
    }
}