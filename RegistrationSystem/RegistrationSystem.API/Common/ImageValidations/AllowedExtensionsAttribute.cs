using System.ComponentModel.DataAnnotations;

namespace RegistrationSystem.API.Common.ImageValidations
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _allowedFileExtensions;

        public AllowedExtensionsAttribute(string[] allowedFileExtensions)
        {
            _allowedFileExtensions = allowedFileExtensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_allowedFileExtensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult($"Only {string.Join(';',_allowedFileExtensions)} file formats are compatible");
                }
            }
            return ValidationResult.Success;
        }
    }
}
