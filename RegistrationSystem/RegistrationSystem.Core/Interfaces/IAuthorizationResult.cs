using RegistrationSystem.Core.Common;

namespace RegistrationSystem.Core.Interfaces
{
    public interface IAuthorizationResult<T>: IResult<T> where T : class
    {
       string? Token { get; set; }
    }
}