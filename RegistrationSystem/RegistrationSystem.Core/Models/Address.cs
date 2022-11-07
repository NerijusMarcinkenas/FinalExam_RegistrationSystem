using System.ComponentModel.DataAnnotations;

namespace RegistrationSystem.Core.Models
{
    public class Address : BaseModel
    {
        [MaxLength(250), Required]
        public string City { get; set; } = string.Empty;

        [MaxLength(250),Required]
        public string Street { get; set; } = string.Empty;

        [MaxLength(25),Required]
        public string BuildingNumber { get; set; } = string.Empty;

        [MaxLength(25)]
        public string? FlatNumber { get; set; } = null;
    }
}