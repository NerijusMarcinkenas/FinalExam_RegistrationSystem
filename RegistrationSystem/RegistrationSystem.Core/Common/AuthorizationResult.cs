using RegistrationSystem.Core.Interfaces;

namespace RegistrationSystem.Core.Common;

internal class AuthorizationResult<T> : Result<T>, IResult<T>, IAuthorizationResult<T> where T : class
{
    public string? Token { get; set; } = null;
}
