using RegistrationSystem.Core.Interfaces;

namespace RegistrationSystem.API.Dtos.AuthorizationDtos
{
    public class AuthorizationResponse<T> where T : class
    {
        public string? Token { get; set; } = null;
        public string? Message { get; set; } = null;

        public AuthorizationResponse()
        {
        }

        public AuthorizationResponse(IAuthorizationResult<T> result)
        {
            Token = result.Token;
            Message = result.Message;
        }
    }
}
