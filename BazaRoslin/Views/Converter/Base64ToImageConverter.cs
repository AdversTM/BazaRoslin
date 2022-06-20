using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using BazaRoslin.Util;

namespace BazaRoslin.Views.Converter {
    [ValueConversion(typeof(string), typeof(ImageSource))]
    public class Base64ToImageConverter : MarkupExtension, IValueConverter {

        private static Base64ToImageConverter? _converter;

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return _converter ??= new Base64ToImageConverter();
        }
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return Image.ToImageSource((string)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}