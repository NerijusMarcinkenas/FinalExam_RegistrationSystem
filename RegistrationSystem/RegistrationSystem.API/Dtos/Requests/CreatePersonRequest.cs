using RegistrationSystem.API.Common.ImageValidations;
using System.ComponentModel.DataAnnotations;

namespace RegistrationSystem.API.Dtos.Requests
{
    public class CreatePersonRequest
    {
        [MaxLength(250), Required]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(250), Required]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(100), Required]
        public string PersonalNumber { get; set; } = string.Empty;

        [MaxLength(50), Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [MaxLength(500), Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        [AllowedExtensions(new[] { ".png", ".jpg", ".jpeg" })]
        public IFormFile PersonImage { get; set; } = null!;

        [MaxLength(250), Required]
        public string City { get; set; } = null!;

        [MaxLength(250), Required]
        public string Street { get; set; } = null!;

        [MaxLength(25), Required]
        public string BuildingNumber { get; set; } = null!;

        [MaxLength(25)]
        public string? FlatNumber { get; set; } = null;

        //public CreateImageRequest CreateImageRequest { get; set; } = null!;
        //public CreateAddressRequest CreateAddressRequest { get; set; } = null!;
    }
}
