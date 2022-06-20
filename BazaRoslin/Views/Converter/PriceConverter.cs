using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace BazaRoslin.Views.Converter {
    [ValueConversion(typeof(decimal), typeof(string))]
    public class PriceConverter : MarkupExtension, IValueConverter {
        
        private static PriceConverter? _converter;

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return _converter ??= new PriceConverter();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return $"{value:F} zł";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}