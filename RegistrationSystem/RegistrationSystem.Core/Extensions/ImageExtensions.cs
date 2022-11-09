using System.Drawing;
using System.Drawing.Imaging;

namespace RegistrationSystem.Core.Extensions
{
    public static class ImageExtensions
    {
        public static byte[] GetImageBytes(this Image image, ImageFormat format)
        {
            using MemoryStream ms = new();
            image.Save(ms, format);
            return ms.ToArray();
        }
    }
}
