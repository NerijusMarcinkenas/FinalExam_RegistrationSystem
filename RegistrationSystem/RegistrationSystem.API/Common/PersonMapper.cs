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
            return new Person
            {
                FirstName = createPersonRequest.FirstName,
                LastName = createPersonRequest.LastName,
                PersonalNumber = createPersonRequest.PersonalNumber,
                Email = createPersonRequest.Email,
                PhoneNumber = createPersonRequest.PhoneNumber,
                UserId = userId,
                Address = MapAddress(createPersonRequest.CreateAddressRequest),
                Image = _imageService.CreateImage(createPersonRequest.CreateImageRequest.PersonImage),                
            };
        }

        static Address MapAddress(CreateAddressRequest addressRequest)
        {
            return new Address
            {
                BuildingNumber = addressRequest.BuildingNumber,
                Street = addressRequest.Street,
                City = addressRequest.City,
                FlatNumber = addressRequest.FlatNumber
            };
        }

    }
}
