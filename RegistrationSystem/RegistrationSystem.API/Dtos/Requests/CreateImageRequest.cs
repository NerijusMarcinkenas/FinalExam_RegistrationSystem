using FilesUploadApiDemo.Validation;
using System.ComponentModel.DataAnnotations;

namespace RegistrationSystem.API.Dtos.Requests
{
    public class CreateImageRequest
    {
        [Required]
        [AllowedExtensions(new[] {".png",".jpg", ".jpeg"})]
        public IFormFile PersonImage { get; set; } = null!;       

    }
}
