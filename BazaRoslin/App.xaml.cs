using System.Windows;
using BazaRoslin.Services;
using BazaRoslin.Services.Mock;
using BazaRoslin.Views;
using Prism.Ioc;

namespace BazaRoslin {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App {
        protected override void RegisterTypes(IContainerRegistry containerRegistry) {
            containerRegistry.RegisterInstance<IUserStore>(new MockUserStore());
            containerRegistry.RegisterInstance<IPlantStore>(new MockPlantStore());

            containerRegistry.RegisterDialog<LoginDialog>("LoginDialog");
        }

        protected override Window CreateShell() {
            return Container.Resolve<Shell>();
        }
    }

    public static class Region {
        public const string TabContentRegion = nameof(TabContentRegion);
        public const string DetailsRegion = nameof(DetailsRegion);
        public const string OffersRegion = nameof(OffersRegion);
    }
}