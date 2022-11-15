using RegistrationSystem.Core.Common;

namespace RegistrationSystem.API.Dtos.AuthorizationDtos
{
    public class AuthorizationResponse<T> where T : class
    {
        public string? Token { get; set; } 
        public string? Message { get; set; } 
        public string? UserId { get; set; } 

        public AuthorizationResponse()
        {
        }

        public AuthorizationResponse(AuthorizationResult<T> result)
        {   
            
            Token = result.Token;
            Message = result.Message;
            UserId = result.UserId;
        }
    }
}
