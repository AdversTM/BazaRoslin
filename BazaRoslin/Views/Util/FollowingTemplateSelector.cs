using System.Windows;
using System.Windows.Controls;

namespace BazaRoslin.Views.Util {
    public class FollowingTemplateSelector : DataTemplateSelector {
        public override DataTemplate? SelectTemplate(object item, DependencyObject container) {
            if (container is not FrameworkElement elem)
                return null;

            if (item is not bool following)
                return null;

            return (DataTemplate)elem.FindResource(following ? "FollowingDataTemplate" : "NotFollowingDataTemplate");
        }
    }
}