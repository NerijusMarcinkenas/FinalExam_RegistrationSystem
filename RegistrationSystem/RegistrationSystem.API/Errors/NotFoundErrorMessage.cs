namespace RegistrationSystem.API.Errors
{
    public class NotFoundErrorMessage
    {
        public string ErrorMessage { get; set; } = string.Empty;
        public NotFoundErrorMessage(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
