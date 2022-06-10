using System;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SDImage = System.Drawing.Image;

namespace BazaRoslin.Util {
    public static class Image {
        public static string ToBase64(string path) {
            var image = File.ReadAllBytes(path);
            return Convert.ToBase64String(image);
        }

        public static SDImage ToImage(string base64) {
            using var stream = new MemoryStream(Convert.FromBase64String(base64));
            return SDImage.FromStream(stream);
        }

        public static ImageSource ToImageSource(string base64) {
            var img = ToImage(base64);
            
            using var ms = new MemoryStream();
            img.Save(ms, ImageFormat.Bmp);
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