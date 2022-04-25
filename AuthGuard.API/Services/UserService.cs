using AuthGuard.API.Contracts;
using AuthGuard.API.Extensions;
using AuthGuard.API.Infrastructure.Constants;
using AuthGuard.API.Models;
using Microsoft.Extensions.Logging;

namespace AuthGuard.API.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        
        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }

        public bool IsValidUserCredentials(TokenRequest token)
        {
            _logger.LogInformation($"Validating user [{token.UserName}]");

            var userAvailable = CheckValidUser(token);
            
            if (!userAvailable)
            {
                _logger.LogInformation($"User [{token.UserName}] username or password are unavailable");

                return false;
            }

            var clientIdAvailable = CheckValidClientId(token);

            if (!clientIdAvailable)
            {
                _logger.LogInformation($"User [{token.UserName}] client id is unavailable");

                return false;
            }

            var clientSecretAvailable = CheckValidClientSecret(token);

            if (!clientSecretAvailable)
            {
                _logger.LogInformation($"User [{token.UserName}] client secret is unavailable");

                return false;
            }

            var grantTypeAvailable = CheckValidGrantType(token);

            if (!grantTypeAvailable)
            {
                _logger.LogInformation($"User [{token.UserName}] client grant type is unavailable");

                return false;
            }

            var clientScopesAvailable = CheckValidClientScopes(token);

            if (!clientScopesAvailable)
            {
                _logger.LogInformation($"User [{token.UserName}] client scopes are unavailable");

                return false;
            }


            return true;
        }
        
        private static bool CheckValidUser(TokenRequest token)
        {
            var userAvailable = Users.List.TryGetValue(token.UserName, out var p) && p == token.Password;

            return userAvailable;
        }     
        
        private static bool CheckValidClientId(TokenRequest token)
        {
            var clientIdAvailable = ClientConstant.ClientId == token.ClientId;

            return clientIdAvailable;
        }    
        
        private static bool CheckValidClientSecret(TokenRequest token)
        {
            var clientSecretAvailable = ClientConstant.ClientSecret == token.ClientSecret;

            return clientSecretAvailable;
        }   
        
        private static bool CheckValidGrantType(TokenRequest token)
        {
            var grantTypeAvailable = ClientConstant.GrantType == token.GrantType;

            return grantTypeAvailable;
        } 
        
        private static bool CheckValidClientScopes(TokenRequest token)
        {
            var clientScopesAvailable = ClientConstant.ClientScopes.CompareLists(token.ClientScopes);

            return clientScopesAvailable;
        }
    }
}