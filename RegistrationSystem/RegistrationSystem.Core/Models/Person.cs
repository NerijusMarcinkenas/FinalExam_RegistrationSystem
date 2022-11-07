using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistrationSystem.Core.Models
{
    public class Person : BaseModel
    {
        [MaxLength(250), Required]
        public string FirstName { get; set; } = null!;

        [MaxLength(250), Required]
        public string LastName { get; set; } = null!;

        [MaxLength(100), Required]
        public string PersonalNumber { get; set; } = null!;

        [MaxLength(50), Required]
        public string PhoneNumber { get; set; } = null!;

        [MaxLength(500),Required]
        public string Email { get; set; } = null!;
        
        [ForeignKey("User")]
        public string UserId { get; set; } = null!;

        public PersonImage Image { get; set; } = null!;
        public Address Address { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}