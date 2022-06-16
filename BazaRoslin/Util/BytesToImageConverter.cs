using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace BazaRoslin.Util {
    [ValueConversion(typeof(byte[]), typeof(ImageSource))]
    public class BytesToImageConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return Image.ToImageSource((byte[])value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}