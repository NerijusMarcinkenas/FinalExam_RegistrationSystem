using Core.Interfaces;
using RegistrationSystem.Core.Models;

namespace RegistrationSystem.DAL.Repositories
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<Person?> GetPersonByUserId(string userId);
        Task<Person?> GetPersonWithIncludesAsync(string userId);
    }
}
