using RegistrationSystem.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace RegistrationSystem.Core.Models
{
    public class User : BaseModel
    {
        [MaxLength(250), Required]
        public string Username { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public Roles Role { get; set; }

        public Person? Person { get; set; } = null;
    }
}
