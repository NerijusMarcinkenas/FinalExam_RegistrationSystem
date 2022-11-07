using RegistrationSystem.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace RegistrationSystem.API.Dtos.Responses
{
    public class PersonResponse
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;      
        public string PersonalNumber { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public AddressResponse Address { get; set; } = null!;
        public ImageResponse Image { get; set; } = null!;

        public PersonResponse()
        {
        }

        public PersonResponse(Person person)
        {
            FirstName = person.FirstName;
            LastName = person.LastName;
            PersonalNumber = person.PersonalNumber;
            PhoneNumber = person.PhoneNumber;
            Email = person.Email;
            MapAddress(person.Address);
            MapImage(person.Image);
        }

        void MapAddress(Address address)
        {
            Address = new AddressResponse
            {
                BuildingNumber = address.BuildingNumber,
                Street = address.Street,
                City = address.City,
                FlatNumber = address.FlatNumber,                
            };
        }

        void MapImage(PersonImage personImage)
        {
            Image = new ImageResponse
            {
                ContentType = personImage.ContentType,
                FileName = personImage.Name,
                ImageBytes = personImage.ImageBytes
            };
        }
    }
}
