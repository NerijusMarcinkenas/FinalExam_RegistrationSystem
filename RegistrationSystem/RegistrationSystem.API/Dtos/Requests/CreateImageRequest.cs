using System.ComponentModel.DataAnnotations;
using RegistrationSystem.API.Common.ImageValidations;

namespace RegistrationSystem.API.Dtos.Requests
{
    public class CreateImageRequest
    {
        [Required]
        [AllowedExtensions(new[] {".png",".jpg", ".jpeg"})]
        public IFormFile PersonImage { get; set; } = null!;       

    }
}
