using JwtApi.Models;
using System.Security.Claims;

namespace JwtApi.Services
{
    public interface IJwtAuthenticationService
    {
        User Authenticate(string email, string password);
        string GenereToken(string secret, List<Claim> claims);
    }
}
