using System;
using System.Windows;
using System.Windows.Controls;
using ImTools;

namespace BazaRoslin.Views.Util {
    public class RatingTemplateSelector : DataTemplateSelector {
        public override DataTemplate? SelectTemplate(object item, DependencyObject container) {
            if (container is not FrameworkElement elem)
                return null;

            if (item is not int rating)
                return null;

            var i = int.Parse(((ContentControl)elem.TemplatedParent).Tag.ToString());

            return (DataTemplate)elem.FindResource(i < rating ? "RatingSolidDataTemplate" : "RatingRegularDataTemplate");
        }
    }
}