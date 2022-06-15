using System.Diagnostics;
using System.Windows;
using BazaRoslin.Services;
using BazaRoslin.Views;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace BazaRoslin.ViewModels {
    public class ShellViewModel : BindableBase {
        public ShellViewModel(IDialogService dialogService, IRegionManager regionManager, IAuthService authService) {
            regionManager.RegisterViewWithRegion<UserView>("TabContentRegion");
            regionManager.RegisterViewWithRegion<CatalogView>("TabContentRegion");
            
            var param = new DialogParameters();
            dialogService.Show("LoginDialog", param, _ => {
                if (!authService.IsLogged) {
                    Application.Current.Shutdown();
                    return;
                }

                var u = authService.LoggedUser;
                Debug.WriteLine($"Logged as {u.Name} {u.Surname}");
                var shell = Application.Current.MainWindow!;
                shell.Show();
                shell.Activate();
            });
        }
    }
}