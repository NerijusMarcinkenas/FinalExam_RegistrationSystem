using System.ComponentModel.DataAnnotations;

namespace RegistrationSystem.API.Dtos.Responses
{
    public class AddressResponse
    {
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string BuildingNumber { get; set; } = string.Empty;
        public string? FlatNumber { get; set; } = null;
    }
}
