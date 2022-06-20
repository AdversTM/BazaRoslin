using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using BazaRoslin.Util;

namespace BazaRoslin.Views.Converter {
    [ValueConversion(typeof(byte[]), typeof(ImageSource))]
    public class BytesToImageConverter : MarkupExtension, IValueConverter {
        private static BytesToImageConverter? _converter;

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return _converter ??= new BytesToImageConverter();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return Image.ToImageSource((byte[])value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}