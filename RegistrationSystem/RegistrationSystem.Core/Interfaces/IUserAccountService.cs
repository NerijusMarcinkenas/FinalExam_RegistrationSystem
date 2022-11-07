using RegistrationSystem.Core.Common;
using RegistrationSystem.Core.Models;

namespace RegistrationSystem.Core.Interfaces
{
    public interface IUserAccountService
    {
        Task<IAuthorizationResult<User>> CreateUserAccountAsync(string username, string password);

        Task<IAuthorizationResult<User>> LoginUserAsync(string username, string password);

        Task<IResult<User>> RemoveUserAccountAsync(string userId);
    }
}
