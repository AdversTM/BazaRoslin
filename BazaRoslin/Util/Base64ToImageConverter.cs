using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace BazaRoslin.Util {
    [ValueConversion(typeof(string), typeof(ImageSource))]
    public class Base64ToImageConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return Image.ToImageSource((string)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}