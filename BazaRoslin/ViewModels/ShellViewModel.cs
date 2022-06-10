using System.Diagnostics;
using System.Windows;
using BazaRoslin.Event;
using BazaRoslin.Model;
using BazaRoslin.Services;
using BazaRoslin.Views;
using DryIoc;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace BazaRoslin.ViewModels {
    public class ShellViewModel : BindableBase {

        private IDialogService _dialogService;
        private IEventAggregator _eventAggregator;
        private IRegionManager _regionManager;

        private IUser? _loggedUser;

        public ShellViewModel(IDialogService dialogService, IEventAggregator eventAggregator, IRegionManager regionManager, IContainer container) {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _dialogService = dialogService;
            
            _eventAggregator.GetEvent<UserLoggedEvent>().Subscribe(user => {
                _loggedUser = user;
                container.RegisterInstance(user, IfAlreadyRegistered.Replace);
            });
            
            _regionManager.RegisterViewWithRegion<UserView>("TabContentRegion");
            _regionManager.RegisterViewWithRegion<CatalogView>("TabContentRegion");
            
            var param = new DialogParameters();
            _dialogService.Show("LoginDialog", param, _ => {
                if (_loggedUser == null) {
                    Application.Current.Shutdown();
                    return;
                }
                Debug.WriteLine($"Logged as {_loggedUser.Name} {_loggedUser.Surname}");
                var shell = container.Resolve<Shell>();
                shell.Show();
                shell.Activate();
            });
        }

        public void OnWindowActivated(Window window) {
            if (_loggedUser != null) return;
            window.Hide();
        }
    }
}