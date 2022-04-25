using System.Collections.Generic;

namespace AuthGuard.API.Infrastructure.Constants
{
    public static class ClientConstant
    {
        public const string ClientId = "roofstacksclient";
        public const string ClientSecret = "roofstackssecret";
        public const string GrantType = "password";
        public static readonly List<string> ClientScopes = new() {"Employee.API"};
    }
}