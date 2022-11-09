using RegistrationSystem.Core.Common;
using RegistrationSystem.Core.Models;

namespace RegistrationSystem.Core.Interfaces
{
    public interface IPersonService
    {
        Task<Result<Person>> AddPersonAsync(Person person);
        Task<Person> UpdatePerson(Person personToUpdate);
        Task<Person?> GetPersonWithIncludesAsync(string userId);
    }
}
