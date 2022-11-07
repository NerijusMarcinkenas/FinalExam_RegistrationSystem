using System.ComponentModel.DataAnnotations;

namespace RegistrationSystem.API.Dtos.Responses
{
    public class ResultResponse<T> where T : class
    {
        public T? ObjectResult { get; set; } = null;
        public string? Message { get; set; } = null;
    }
}
