using RegistrationSystem.Core.Models;

namespace RegistrationSystem.Core.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetUserByUsername(string username);
        Task<User?> GetUserWithPerson(string userId);
        Task<bool> IsUserExists(string username);
    }
}
