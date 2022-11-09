using RegistrationSystem.Core.Common;
using RegistrationSystem.Core.Models;

namespace RegistrationSystem.Core.Interfaces
{
    public interface IUserAccountService
    {
        Task<AuthorizationResult<User>> CreateUserAccountAsync(string username, string password);

        Task<AuthorizationResult<User>> LoginUserAsync(string username, string password);

        Task<Result<User>> RemoveUserAccountAsync(string userId);
    }
}
