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
        
        public CreateImageRequest CreateImageRequest { get; set; } = null!;
        public CreateAddressRequest CreateAddressRequest { get; set; } = null!;
    }
}
