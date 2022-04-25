using System.Collections.Generic;

namespace AuthGuard.API.Infrastructure.Constants
{
    public static class Users
    {
        public static readonly IDictionary<string, string> List = new Dictionary<string, string>
        {
            {"agit", "test1234"},
            {"elgun", "qwerty"}
        };
    }
}