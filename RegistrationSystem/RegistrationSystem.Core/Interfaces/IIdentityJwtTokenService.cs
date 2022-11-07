using RegistrationSystem.Core.Models;

namespace RegistrationSystem.Core.Interfaces
{
    internal interface IIdentityJwtTokenService
    {
        string GetJwtToken(User user);
    }
}