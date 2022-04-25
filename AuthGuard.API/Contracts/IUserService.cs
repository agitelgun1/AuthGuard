using AuthGuard.API.Models;

namespace AuthGuard.API.Contracts
{
    public interface IUserService
    {
        bool IsValidUserCredentials(TokenRequest token);
    }
}