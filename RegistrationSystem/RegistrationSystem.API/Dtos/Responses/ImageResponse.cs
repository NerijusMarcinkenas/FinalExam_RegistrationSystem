namespace RegistrationSystem.API.Dtos.Responses
{
    public class ImageResponse
    {
        public string FileName { get; set; } = null!;
        public byte[] ImageBytes { get; set; } = null!;
        public string ContentType { get; set; } = null!;
    }
}
