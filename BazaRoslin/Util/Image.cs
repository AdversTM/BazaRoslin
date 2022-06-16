using System;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SDImage = System.Drawing.Image;

namespace BazaRoslin.Util {
    public static class Image {
        public static byte[] GetBytes(string path) {
            return File.ReadAllBytes(path);
        }

        public static string ToBase64(string path) {
            return Convert.ToBase64String(GetBytes(path));
        }

        public static SDImage ToImage(byte[] bytes) {
            using var stream = new MemoryStream(bytes);
            return SDImage.FromStream(stream);
        }

        public static SDImage ToImage(string base64) {
            return ToImage(Convert.FromBase64String(base64));
        }

        public static ImageSource ToImageSource(byte[] bytes) {
            return ToImageSource(ToImage(bytes));
        }

        public static ImageSource ToImageSource(string base64) {
            return ToImageSource(ToImage(base64));
        }

        public static ImageSource ToImageSource(SDImage image) {
            using var ms = new MemoryStream();
            image.Save(ms, ImageFormat.Bmp);
            ms.Seek(0, SeekOrigin.Begin);

            var bmImage = new BitmapImage();
            bmImage.BeginInit();
            bmImage.CacheOption = BitmapCacheOption.OnLoad;
            bmImage.StreamSource = ms;
            bmImage.EndInit();
            return bmImage;
        }
    }
}