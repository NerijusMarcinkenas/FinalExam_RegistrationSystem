using RegistrationSystem.API.Dtos.Requests;
using RegistrationSystem.API.Dtos.Responses;
using RegistrationSystem.Core.Models;

namespace RegistrationSystem.API.Common
{
    public static class PersonMapper
    {
        public static Person MapPersonRequest(CreatePersonRequest createPersonRequest, string userId)
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
                Image = ImageHelper.CreateImage(createPersonRequest.CreateImageRequest),                
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
