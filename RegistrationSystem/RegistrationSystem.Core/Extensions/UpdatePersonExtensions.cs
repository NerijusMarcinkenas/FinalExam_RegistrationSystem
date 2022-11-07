using RegistrationSystem.Core.Interfaces;
using RegistrationSystem.Core.Models;

namespace RegistrationSystem.Core.Extensions
{
    public static class UpdatePersonExtensions
    {
        public static async Task<Person> UpdateFirstNameAsync(this IPersonService service, Person person, string value)
        {
            person.FirstName = value;
            await service.UpdatePerson(person);
            return person;
        }

        public static async Task<Person> UpdateLastNameAsync(this IPersonService service, Person person, string value)
        {
            person.LastName = value;
            await service.UpdatePerson(person);
            return person;
        }
        public static async Task<Person> UpadatePersonalNumberAsync(this IPersonService service, Person person, string value)
        {
            person.PersonalNumber = value;
            await service.UpdatePerson(person);
            return person;
        }

        public static async Task<Person> UpdatePhoneNumberAsync(this IPersonService service, Person person, string value)
        {
            person.PhoneNumber = value;
            await service.UpdatePerson(person);
            return person;
        }

        public static async Task<Person> UpdateEmailAsync(this IPersonService service, Person person, string value)
        {
            person.Email = value;
            await service.UpdatePerson(person);
            return person;
        }
        public static async Task<Person> UpadateImageAsync(this IPersonService service, Person person, PersonImage value)
        {
            person.Image = value;
            await service.UpdatePerson(person);
            return person;
        }
        public static async Task<Person> UpdateAddressCityAsync(this IPersonService service, Person person, string value)
        {
            person.Address.City = value;
            await service.UpdatePerson(person);
            return person;
        }

        public static async Task<Person> UpdateAddressStreetAsync(this IPersonService service, Person person, string value)
        {
            person.Address.Street = value;
            await service.UpdatePerson(person);
            return person;
        }

        public static async Task<Person> UpdateAddressBuildingNumberAsync(this IPersonService service, Person person, string value)
        {
            person.Address.BuildingNumber = value;
            await service.UpdatePerson(person);
            return person;
        }

        public static async Task<Person> UpdateAddressFlatNumberAsync(this IPersonService service, Person person, string value)
        {
            person.Address.FlatNumber = value;
            await service.UpdatePerson(person);
            return person;
        }
    }
}
