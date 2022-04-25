using System;
using System.Security.Claims;
using AuthGuard.API.Models;

namespace AuthGuard.API.Contracts
{
    public interface IAuthManager
    {
        AuthResult GenerateTokens(Claim[] claims);
    }
}