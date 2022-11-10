using RegistrationSystem.Core.Models;

namespace RegistrationSystem.Core.Interfaces
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<Person?> GetPersonByUserId(string userId);
        Task<Person?> GetPersonWithIncludesAsync(string userId);
    }
}
