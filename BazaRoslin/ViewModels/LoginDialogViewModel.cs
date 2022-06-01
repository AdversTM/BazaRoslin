using System.Windows;
using System.Windows.Input;
using BazaRoslin.Event;
using BazaRoslin.Model;
using BazaRoslin.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;

namespace BazaRoslin.ViewModels {
    public class LoginDialogViewModel : DialogViewModelBase {
        private readonly IUserStore _userStore;
        private readonly IEventAggregator _eventAggregator;

        private string _login = "";
        private string _password = "";
        private ICommand? _loginCommand;

        private IUser? _loggedUser;

        public string Login {
            get => _login;
            set {
                _login = value;
                RaisePropertyChanged();
            }
        }

        public string Password {
            get => _password;
            set {
                _password = value;
                RaisePropertyChanged();
            }
        }

        public ICommand LoginCommand => _loginCommand ??=
            new DelegateCommand(TryLogin, () => Login.Length > 0 && Password.Length > 0)
                .ObservesProperty(() => Login)
                .ObservesProperty(() => Password);

        public LoginDialogViewModel(IUserStore userStore, IEventAggregator eventAggregator) {
            _userStore = userStore;
            _eventAggregator = eventAggregator;
            
            Title = "Logowanie";
        }

        public override void OnDialogOpened(IDialogParameters parameters) {
            // Message = parameters.GetValue<string>("message");
        }

        private async void TryLogin() {
            var u = await _userStore.GetUser(Login);
            if (u != null && u.CheckPassword(Password)) {
                _loggedUser = u;
                _eventAggregator.GetEvent<UserLoggedEvent>().Publish(_loggedUser);
                CloseDialog();
                return;
            }

            MessageBox.Show("Podane dane są niepoprawne!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        // public override bool CanCloseDialog() {
            // return _loggedUser != null;
        // }

        private void CloseDialog() {
            RaiseRequestClose(new DialogResult());
        }
    }
}