using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace BazaRoslin.Views.Converter {
    [ValueConversion(typeof(object), typeof(string))]
    public class ToStringConverter : MarkupExtension, IValueConverter {
        
        private static ToStringConverter? _converter;

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return _converter ??= new ToStringConverter();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}