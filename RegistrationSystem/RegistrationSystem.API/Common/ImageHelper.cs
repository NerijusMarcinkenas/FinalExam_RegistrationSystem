using RegistrationSystem.API.Dtos.Requests;
using RegistrationSystem.Core.Models;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace RegistrationSystem.API.Common
{
    public static class ImageHelper
    {
        public static PersonImage CreateImage(CreateImageRequest imageRequest)
        {            

            using var memmoryStream = new MemoryStream();
            imageRequest.PersonImage.CopyTo(memmoryStream);
            var imageBytes = memmoryStream.ToArray();

            var resizedImageBytes = ResizeImage(imageBytes, 200,200).GetImageBytes(ImageFormat.Jpeg);
            
            return new PersonImage
            {
                Name = imageRequest.PersonImage.FileName,
                ContentType = imageRequest.PersonImage.ContentType,
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

        private static byte[] GetImageBytes(this Image image, ImageFormat format)
        {
            using MemoryStream ms = new();
            image.Save(ms, format);
            return ms.ToArray();
        }

    }
}
