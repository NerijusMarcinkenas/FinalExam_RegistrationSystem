using RegistrationSystem.Core.Interfaces;

namespace RegistrationSystem.Core.Common;

public class AuthorizationResult<T> : Result<T> where T : class
{
    public string? Token { get; set; } = null;
    public string? UserId { get; set; } = null;
}
