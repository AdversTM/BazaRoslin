using System.Diagnostics;
using BazaRoslin.Event;
using BazaRoslin.Model;
using BazaRoslin.Services;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace BazaRoslin.ViewModels {
    public class ShellViewModel : BindableBase {

        private IDialogService _dialogService;
        private IEventAggregator _eventAggregator;
        private IUserStore _userStore;

        private IUser _loggedUser;

        public ShellViewModel(IDialogService dialogService, IEventAggregator eventAggregator, IUserStore userStore) {
            _userStore = userStore;
            _eventAggregator = eventAggregator;
            _dialogService = dialogService;

            _eventAggregator.GetEvent<UserLoggedEvent>().Subscribe(user => _loggedUser = user);

            var param = new DialogParameters();
            _dialogService.Show("LoginDialog", param, r => {
                Debug.WriteLine($"Logged as {_loggedUser!.Name} {_loggedUser.Surname}");
            });
        }
    }
}