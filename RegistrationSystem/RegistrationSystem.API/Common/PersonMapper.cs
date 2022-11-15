using RegistrationSystem.API.Dtos.Requests;
using RegistrationSystem.Core.Models;
using RegistrationSystem.Core.Services;

namespace RegistrationSystem.API.Common
{
    public interface IPersonMaper
    {
        Person MapPersonRequest(CreatePersonRequest createPersonRequest, string userId);
    }

    public class PersonMapper : IPersonMaper
    {
        private readonly IImageService _imageService;

        public PersonMapper(IImageService imageService)
        {
            _imageService = imageService;
        }

        public Person MapPersonRequest(CreatePersonRequest createPersonRequest, string userId)
        {
            var person = new Person
            {
                FirstName = createPersonRequest.FirstName,
                LastName = createPersonRequest.LastName,
                PersonalNumber = createPersonRequest.PersonalNumber,
                Email = createPersonRequest.Email,
                PhoneNumber = createPersonRequest.PhoneNumber,
                UserId = userId,
                Address = new Address
                {
                    BuildingNumber = createPersonRequest.BuildingNumber,
                    Street = createPersonRequest.Street,
                    City = createPersonRequest.City,
                    FlatNumber = createPersonRequest.FlatNumber
                }                            
            };

            person.Image = _imageService.CreateImage(createPersonRequest.PersonImage, person);
            return person;
        }
    }
}
