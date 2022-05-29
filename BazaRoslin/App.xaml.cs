using System.Windows;
using BazaRoslin.Services;
using BazaRoslin.Views;
using Prism.Ioc;

namespace BazaRoslin {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App {
        protected override void RegisterTypes(IContainerRegistry containerRegistry) {
            containerRegistry.RegisterInstance<IUserStore>(new MockUserStore());

            containerRegistry.RegisterDialog<LoginDialog>("LoginDialog");
        }

        protected override Window CreateShell() {
            return Container.Resolve<Shell>();
        }
    }
}