namespace RegistrationSystem.Core.Common
{
    public interface IResult<T> where T : class
    {
        public bool IsSuccess { get; set; }
        string Message { get; set; }
        T? ResultObject { get; set; }
    }
}