using System.Security.Claims;
using AuthGuard.API.Contracts;
using AuthGuard.API.Controllers;
using AuthGuard.API.Models;
using AuthGuard.API.Models.Config;
using AuthGuard.API.Tests.TestData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace AuthGuard.API.Tests.Controllers
{
    public class AuthenticationControllerTests
    {
        [Fact]
        public void GenerateToken_Should_Return_As_Expected()
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

            var app = new TokenConfig
            {
                AccessTokenExpiration = TokenConfigTestData.AccessTokenExpiration,
                Issuer = TokenConfigTestData.Issuer,
                Audience = TokenConfigTestData.Audience,
                Secret = TokenConfigTestData.Secret
            };

            var token = TokenConfigTestData.Token;
            
            var mockLogger = new Mock<ILogger<AuthenticationController>>();
            var mockUserService = new Mock<IUserService>();
            var mockTokenConfig = new Mock<IOptions<TokenConfig>>();
            var mockAuthManager = new Mock<IAuthManager>();

            mockUserService.Setup(x => x.IsValidUserCredentials(tokenRequest))
                .Returns(true);

            mockTokenConfig.Setup(ap => ap.Value).Returns(app);

            mockAuthManager.Setup(x => x.GenerateTokens(It.IsAny<Claim[]>()))
                .Returns(new AuthResult
                {
                    AccessToken = token
                });

            var authenticationController = new AuthenticationController(mockLogger.Object, mockUserService.Object,
                mockAuthManager.Object, mockTokenConfig.Object);
            var result = authenticationController.Token(tokenRequest);

            var response = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<TokenResult>(response.Value);
           
            mockAuthManager.Verify(r => r.GenerateTokens(It.IsAny<Claim[]>()), Times.Once);
            mockUserService.Verify(r => r.IsValidUserCredentials(It.IsAny<TokenRequest>()), Times.Once);

            Assert.Equal(tokenRequest.UserName, model.UserName);
            Assert.Equal(token, model.AccessToken);
        } 
        
        [Fact]
        public void GenerateToken_Should_Return_As_UnExpected()
        {
            var tokenRequest = new TokenRequest
            {
                UserName = "test",
                Password = UserCredentialTestData.Password,
                ClientId = UserCredentialTestData.ClientId,
                ClientSecret = UserCredentialTestData.ClientSecret,
                ClientScopes = UserCredentialTestData.ClientScopes,
                GrantType = UserCredentialTestData.GrantType
            };

            var app = new TokenConfig
            {
                AccessTokenExpiration = TokenConfigTestData.AccessTokenExpiration,
                Issuer = TokenConfigTestData.Issuer,
                Audience = TokenConfigTestData.Audience,
                Secret = TokenConfigTestData.Secret
            };

            var token = TokenConfigTestData.Token;
            
            var mockLogger = new Mock<ILogger<AuthenticationController>>();
            var mockUserService = new Mock<IUserService>();
            var mockTokenConfig = new Mock<IOptions<TokenConfig>>();
            var mockAuthManager = new Mock<IAuthManager>();

            mockUserService.Setup(x => x.IsValidUserCredentials(tokenRequest))
                .Returns(false);

            mockTokenConfig.Setup(ap => ap.Value).Returns(app);

            mockAuthManager.Setup(x => x.GenerateTokens(It.IsAny<Claim[]>()))
                .Returns(new AuthResult
                {
                    AccessToken = token
                });

            var authenticationController = new AuthenticationController(mockLogger.Object, mockUserService.Object,
                mockAuthManager.Object, mockTokenConfig.Object);
            var result = authenticationController.Token(tokenRequest);

            mockAuthManager.Verify(r => r.GenerateTokens(It.IsAny<Claim[]>()), Times.Never);
            mockUserService.Verify(r => r.IsValidUserCredentials(It.IsAny<TokenRequest>()), Times.Once);
            
            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}