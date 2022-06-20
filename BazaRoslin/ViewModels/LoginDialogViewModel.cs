using System;
using System.Windows;
using System.Windows.Input;
using BazaRoslin.Event;
using BazaRoslin.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;

namespace BazaRoslin.ViewModels {
    public class LoginDialogViewModel : DialogViewModelBase {
        private readonly IUserStore _userStore;
        private readonly IEventAggregator _eventAggregator;
        private readonly SubscriptionToken _eventSubscription;

        private string _login = "";
        private string _password = "";
        private ICommand? _loginCommand;

        public string Login {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        public string Password {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ICommand LoginCommand => _loginCommand ??=
            new DelegateCommand(TryLogin, () => Login.Length > 0 && Password.Length > 0)
                .ObservesProperty(() => Login)
                .ObservesProperty(() => Password);

        public LoginDialogViewModel(IUserStore userStore, IEventAggregator eventAggregator) {
            _userStore = userStore;
            _eventAggregator = eventAggregator;
            Title = "Logowanie";

            var e = _eventAggregator.GetEvent<UserPreLoginEvent>();
            _eventSubscription = e.Subscribe(args => {
                switch (args.Phase) {
                    case LoginPhase.Request:
                        break;
                    case LoginPhase.Response:
                        e.Publish(new LoginArgs(args.User, LoginPhase.Success));
                        CloseDialog();
                        break;
                    case LoginPhase.Success:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }, true);
        }

        public override void OnDialogOpened(IDialogParameters parameters) {
        }

        public override void OnDialogClosed() {
            _eventSubscription.Dispose();
        }

        private async void TryLogin() {
            var u = await _userStore.GetUser(Login);
            if (u != null && u.CheckPassword(Password)) {
                _eventAggregator.GetEvent<UserPreLoginEvent>().Publish(new LoginArgs(u, LoginPhase.Request));
                return;
            }

            MessageBox.Show("Podane dane są niepoprawne!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void CloseDialog() {
            RaiseRequestClose(new DialogResult());
        }
    }
}