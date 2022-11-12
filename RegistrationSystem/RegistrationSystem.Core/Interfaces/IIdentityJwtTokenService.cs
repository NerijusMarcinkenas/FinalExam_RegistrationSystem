using RegistrationSystem.Core.Models;

namespace RegistrationSystem.Core.Interfaces
{
    public interface IIdentityJwtTokenService
    {
        string GetJwtToken(User user);
    }
}