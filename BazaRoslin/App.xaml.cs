using System.Windows;
using BazaRoslin.Services;
using BazaRoslin.Services.Impl;
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
            // containerRegistry.RegisterInstance<IUserStore>(new UserDbContext(Configuration.ConnectionString));
            // containerRegistry.RegisterInstance<IPlantStore>(new PlantDbContext(Configuration.ConnectionString));

            containerRegistry.RegisterSingleton<IAuthService, AuthService>();

            containerRegistry.RegisterDialog<LoginDialog>();
            containerRegistry.RegisterDialog<OfferDialog>();
            
            containerRegistry.RegisterForNavigation<DetailsView>();
            containerRegistry.RegisterForNavigation<OffersView>();
            containerRegistry.RegisterForNavigation<UserDetailsView>();
        }

        protected override Window CreateShell() {
            return Container.Resolve<Shell>();
        }

        protected override void OnInitialized() {
        }
    }

    public static class Region {
        public const string TabContentRegion = nameof(TabContentRegion);
        public const string DetailsRegion = nameof(DetailsRegion);
        public const string OffersRegion = nameof(OffersRegion);
        public const string UserDetailsRegion = nameof(UserDetailsRegion);
    }
}