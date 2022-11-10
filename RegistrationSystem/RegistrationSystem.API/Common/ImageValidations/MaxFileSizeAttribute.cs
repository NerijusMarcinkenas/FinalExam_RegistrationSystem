using System.ComponentModel.DataAnnotations;

namespace RegistrationSystem.API.Common.ImageValidations
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult($"Max file size is allowed {_maxFileSize} bytes");
                }
            }

            return ValidationResult.Success;
        }
    }
}
