namespace RegistrationSystem.API.Errors
{
    public static class AdminCannotRemoveSelf
    {
        public static string Message { get; set; } = "Removing Admin from same account is not allowed!";
    }
}
