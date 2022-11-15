using System.ComponentModel.DataAnnotations;

namespace RegistrationSystem.API.Dtos.AuthorizationDtos
{
    public class RegisterUserRequest
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare("Password", ErrorMessage = "Passwords doesn't match")]
        public string VerifyPassword { get; set; } = null!;
    }
}
