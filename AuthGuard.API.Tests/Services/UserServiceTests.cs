using System.Collections.Generic;
using AuthGuard.API.Models;
using AuthGuard.API.Services;
using AuthGuard.API.Tests.TestData;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AuthGuard.API.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public void IsValidUserCredentials_Should_Return_True_With_TokenRequest()
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
            var mockLogger = new Mock<ILogger<UserService>>();

            var userService = new UserService(mockLogger.Object);

            var result = userService.IsValidUserCredentials(tokenRequest);

            var response = Assert.IsType<bool>(result);
            var model = Assert.IsAssignableFrom<bool>(response);

            Assert.True(model);
        }

        [Fact]
        public void IsValidUserCredentials_Should_Return_False_With_InvalidUserName()
        {
            var tokenRequest = new TokenRequest
            {
                UserName = "invaliduser",
                Password = UserCredentialTestData.Password,
                ClientId = UserCredentialTestData.ClientId,
                ClientSecret = UserCredentialTestData.ClientSecret,
                ClientScopes = UserCredentialTestData.ClientScopes,
                GrantType = UserCredentialTestData.GrantType
            };
            var mockLogger = new Mock<ILogger<UserService>>();

            var userService = new UserService(mockLogger.Object);

            var result = userService.IsValidUserCredentials(tokenRequest);

            var response = Assert.IsType<bool>(result);
            var model = Assert.IsAssignableFrom<bool>(response);

            Assert.False(model);
        }

        [Fact]
        public void IsValidUserCredentials_Should_Return_False_With_InvalidPassword()
        {
            var tokenRequest = new TokenRequest
            {
                UserName = UserCredentialTestData.UserName,
                Password = "invalidpassword",
                ClientId = UserCredentialTestData.ClientId,
                ClientSecret = UserCredentialTestData.ClientSecret,
                ClientScopes = UserCredentialTestData.ClientScopes,
                GrantType = UserCredentialTestData.GrantType
            };
            var mockLogger = new Mock<ILogger<UserService>>();

            var userService = new UserService(mockLogger.Object);

            var result = userService.IsValidUserCredentials(tokenRequest);

            var response = Assert.IsType<bool>(result);
            var model = Assert.IsAssignableFrom<bool>(response);

            Assert.False(model);
        }

        [Fact]
        public void IsValidUserCredentials_Should_Return_False_With_InvalidClientId()
        {
            var tokenRequest = new TokenRequest
            {
                UserName = UserCredentialTestData.UserName,
                Password = UserCredentialTestData.Password,
                ClientId = "invalidclientid",
                ClientSecret = UserCredentialTestData.ClientSecret,
                ClientScopes = UserCredentialTestData.ClientScopes,
                GrantType = UserCredentialTestData.GrantType
            };
            var mockLogger = new Mock<ILogger<UserService>>();

            var userService = new UserService(mockLogger.Object);

            var result = userService.IsValidUserCredentials(tokenRequest);

            var response = Assert.IsType<bool>(result);
            var model = Assert.IsAssignableFrom<bool>(response);

            Assert.False(model);
        }

        [Fact]
        public void IsValidUserCredentials_Should_Return_False_With_InvalidClientSecret()
        {
            var tokenRequest = new TokenRequest
            {
                UserName = UserCredentialTestData.UserName,
                Password = UserCredentialTestData.Password,
                ClientId = UserCredentialTestData.ClientId,
                ClientSecret = "invalidclientsecret",
                ClientScopes = UserCredentialTestData.ClientScopes,
                GrantType = UserCredentialTestData.GrantType
            };
            var mockLogger = new Mock<ILogger<UserService>>();

            var userService = new UserService(mockLogger.Object);

            var result = userService.IsValidUserCredentials(tokenRequest);

            var response = Assert.IsType<bool>(result);
            var model = Assert.IsAssignableFrom<bool>(response);

            Assert.False(model);
        }

        [Fact]
        public void IsValidUserCredentials_Should_Return_False_With_InvalidClientScopes()
        {
            var tokenRequest = new TokenRequest
            {
                UserName = UserCredentialTestData.UserName,
                Password = UserCredentialTestData.Password,
                ClientId = UserCredentialTestData.ClientId,
                ClientSecret = UserCredentialTestData.ClientSecret,
                ClientScopes = new List<string> {"invalidclientsecret"},
                GrantType = UserCredentialTestData.GrantType
            };
            var mockLogger = new Mock<ILogger<UserService>>();

            var userService = new UserService(mockLogger.Object);

            var result = userService.IsValidUserCredentials(tokenRequest);

            var response = Assert.IsType<bool>(result);
            var model = Assert.IsAssignableFrom<bool>(response);

            Assert.False(model);
        }

        [Fact]
        public void IsValidUserCredentials_Should_Return_False_With_InvalidGrantType()
        {
            var tokenRequest = new TokenRequest
            {
                UserName = UserCredentialTestData.UserName,
                Password = UserCredentialTestData.Password,
                ClientId = UserCredentialTestData.ClientId,
                ClientSecret = UserCredentialTestData.ClientSecret,
                ClientScopes = UserCredentialTestData.ClientScopes,
                GrantType = "invalidgranttype"
            };
            var mockLogger = new Mock<ILogger<UserService>>();

            var userService = new UserService(mockLogger.Object);

            var result = userService.IsValidUserCredentials(tokenRequest);

            var response = Assert.IsType<bool>(result);
            var model = Assert.IsAssignableFrom<bool>(response);

            Assert.False(model);
        }
    }
}