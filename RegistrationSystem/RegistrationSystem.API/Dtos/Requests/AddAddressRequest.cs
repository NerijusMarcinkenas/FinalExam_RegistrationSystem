using System.ComponentModel.DataAnnotations;

namespace RegistrationSystem.API.Dtos.Requests
{
    public class CreateAddressRequest
    {
        [MaxLength(250), Required]
        public string City { get; set; } = null!;

        [MaxLength(250), Required]
        public string Street { get; set; } = null!;

        [MaxLength(25), Required]
        public string BuildingNumber { get; set; } = null!;

        [MaxLength(25)]
        public string? FlatNumber { get; set; } = null;
    }
}
