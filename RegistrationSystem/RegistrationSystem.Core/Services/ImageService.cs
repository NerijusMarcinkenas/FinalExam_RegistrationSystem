using Microsoft.AspNetCore.Http;
using RegistrationSystem.Core.Extensions;
using RegistrationSystem.Core.Models;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace RegistrationSystem.Core.Services
{
    public interface IImageService
    {
        PersonImage CreateImage(IFormFile personImage);
    }

    public class ImageService : IImageService
    {
        public PersonImage CreateImage(IFormFile personImage)
        {
            using var memmoryStream = new MemoryStream();
            personImage.CopyTo(memmoryStream);
            var imageBytes = memmoryStream.ToArray();

            var resizedImageBytes = ResizeImage(imageBytes, 200, 200).GetImageBytes(ImageFormat.Jpeg);

            return new PersonImage
            {
                Name = personImage.FileName,
                ContentType = personImage.ContentType,
                ImageBytes = resizedImageBytes,
            };
        }

        private static Bitmap ResizeImage(
            byte[] pictureBytes,
            int width,
            int height)
        {
            using var memstr = new MemoryStream(pictureBytes, 0, pictureBytes.Length);
            Image image = Image.FromStream(memstr);

            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using var wrapMode = new ImageAttributes();
                wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
            }

            return destImage;
        }       
    }
}
