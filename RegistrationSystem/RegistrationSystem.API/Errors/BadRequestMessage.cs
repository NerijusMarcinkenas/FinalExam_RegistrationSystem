namespace RegistrationSystem.API.Errors
{
    public partial class BadRequestMessage
    {
        public string ErrorMessage { get; set; } = string.Empty;
        public BadRequestMessage()
        {
        }

        public BadRequestMessage(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
