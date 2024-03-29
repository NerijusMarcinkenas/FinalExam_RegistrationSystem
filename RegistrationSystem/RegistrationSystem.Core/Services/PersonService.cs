﻿
using RegistrationSystem.Core.Common;
using RegistrationSystem.Core.Interfaces;
using RegistrationSystem.Core.Models;

namespace RegistrationSystem.Core.Services
{

    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
              
        public async Task<Person?> GetPersonWithIncludesAsync(string userId)
        {
            return await _personRepository.GetPersonWithIncludesAsync(userId);
        }

        public async Task<Result<Person>> AddPersonAsync(Person person)
        {
            var result = new Result<Person>();
            var personExists = await _personRepository.GetByIdAsync(person.UserId) != null;
            if (personExists)
            {
                result.Message = "Person information could be added only one time";
                return result;
            }

            var addedPerson = await _personRepository.AddAsync(person);

            result.ResultObject = addedPerson;
            result.IsSuccess = true;
            result.Message = "Person added successfully";

            return result;
        }

        public async Task<Person> UpdatePerson(Person personToUpdate)
        {   
            return await _personRepository.UpdateAsync(personToUpdate);
        }
    }
}
