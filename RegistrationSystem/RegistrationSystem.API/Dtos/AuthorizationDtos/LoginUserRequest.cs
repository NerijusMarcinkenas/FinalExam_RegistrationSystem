using System.ComponentModel.DataAnnotations;

namespace RegistrationSystem.API.Dtos.AuthorizationDtos
{
    public class LoginUserRequest
    {

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
