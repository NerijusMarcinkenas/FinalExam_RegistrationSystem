namespace RegistrationSystem.API.Errors
{
    public class NotFoundErrorMessage
    {
        public string Message { get; set; } = string.Empty;
        public NotFoundErrorMessage(string message)
        {
            Message = message;
        }
    }
}
