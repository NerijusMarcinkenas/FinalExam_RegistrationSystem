using RegistrationSystem.Core.Models;

namespace RegistrationSystem.Core.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> GetUserWithPersonAsync(string userId);    
        Task<bool> IsUserExistsAsync(string username);
    }
}
