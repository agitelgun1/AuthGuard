using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AuthGuard.API.Contracts;
using AuthGuard.API.Models;
using AuthGuard.API.Models.Config;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthGuard.API.Infrastructure.AuthenticationManager
{
    public class AuthManager : IAuthManager
    {
        private readonly TokenConfig _tokenConfig;
        
        public AuthManager(IOptions<TokenConfig> tokenConfig)
        {
            _tokenConfig = tokenConfig.Value;
        }
        
        public AuthResult GenerateTokens(Claim[] claims)
        {
            var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);
            var jwtToken = new JwtSecurityToken(
                _tokenConfig.Issuer,
                shouldAddAudienceClaim ? _tokenConfig.Audience : string.Empty,
                claims,
                expires: DateTime.Now.AddMinutes(_tokenConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenConfig.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            
            return new AuthResult
            {
                AccessToken = accessToken
            };
        }
    }
}