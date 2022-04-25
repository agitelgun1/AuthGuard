using System.Security.Claims;
using AuthGuard.API.Infrastructure.AuthenticationManager;
using AuthGuard.API.Models;
using AuthGuard.API.Models.Config;
using AuthGuard.API.Tests.TestData;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace AuthGuard.API.Tests.Infrastructure.AuthenticationManager
{
    public class AuthManagerTests
    {
        [Fact]
        public void GenerateToken_Should_Return_As_UnExpected()
        {
            var tokenRequest = new TokenRequest
            {
                UserName = UserCredentialTestData.UserName,
                Password = UserCredentialTestData.Password,
                ClientId = UserCredentialTestData.ClientId,
                ClientSecret = UserCredentialTestData.ClientSecret,
                ClientScopes = UserCredentialTestData.ClientScopes,
                GrantType = UserCredentialTestData.GrantType
            };
            
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, tokenRequest.UserName)
            };
            
            var app = new TokenConfig
            {
                AccessTokenExpiration = TokenConfigTestData.AccessTokenExpiration,
                Issuer = TokenConfigTestData.Issuer,
                Audience = TokenConfigTestData.Audience,
                Secret = TokenConfigTestData.Secret
            };
            
            var mockTokenConfig = new Mock<IOptions<TokenConfig>>();
            mockTokenConfig.Setup(ap => ap.Value).Returns(app);

            var authManager= new AuthManager(mockTokenConfig.Object);
            var result = authManager.GenerateTokens(claims);

            var response = Assert.IsType<AuthResult>(result);
            var model = Assert.IsAssignableFrom<AuthResult>(response);
            
            Assert.NotNull(model.AccessToken); 
            Assert.NotEmpty(model.AccessToken); 
        }
    }
}