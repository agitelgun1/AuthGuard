using System.Collections.Generic;

namespace AuthGuard.API.Tests.TestData
{
    public static class TokenConfigTestData
    {
        public static readonly int AccessTokenExpiration = 20;
        public static readonly string Issuer = "https://roofstackstestcase.com";
        public static readonly string Audience = "https://roofstackstestcase.com";
        public static readonly string Secret = "1234567890123456789";
        public static readonly string Token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiZWxndW4iLCJleHAiOjE2NTA4MzE4OTAsImlzcyI6Imh0dHBzOi8vcm9vZnN0YWNrc3Rlc3RjYXNlLmNvbSIsImF1ZCI6Imh0dHBzOi8vcm9vZnN0YWNrc3Rlc3RjYXNlLmNvbSJ9.U_iOA0Fawm-SlocOqpDUCyjlH5rQJw3X4E0TjMlepd4";
    }
    
    public static class UserCredentialTestData
    {
        public static readonly string UserName = "elgun";
        public static readonly string Password = "qwerty";
        public static readonly string ClientId = "roofstacksclient";
        public static readonly string ClientSecret = "roofstackssecret";
        public static readonly List<string> ClientScopes = new List<string> {"Employee.API"};
        public static readonly string GrantType = "password";
    }
}