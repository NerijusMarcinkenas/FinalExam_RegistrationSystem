namespace RegistrationSystem.Core.Common
{
    internal class Result<T> : IResult<T> where T : class
    {
        public T? ResultObject { get; set; } = null;
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; } = false;
    }
}
