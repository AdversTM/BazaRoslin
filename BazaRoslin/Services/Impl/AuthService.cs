using System;
using BazaRoslin.Event;
using BazaRoslin.Model;
using Prism.Events;

namespace BazaRoslin.Services.Impl {
    public class AuthService : IAuthService {
        public IUser? CurrentUser { get; private set; }
        
        public IUser LoggedUser => CurrentUser!;
        public bool IsLogged => CurrentUser != null;

        public AuthService(IEventAggregator eventAggregator) {
            var e = eventAggregator.GetEvent<UserPreLoginEvent>();
            e.Subscribe(args => {
                switch (args.Phase) {
                    case LoginPhase.Request:
                        CurrentUser = args.User;
                        e.Publish(new LoginArgs(CurrentUser, LoginPhase.Response));
                        break;
                    case LoginPhase.Response:
                        break;
                    case LoginPhase.Success:
                        eventAggregator.GetEvent<UserLoggedEvent>().Publish(LoggedUser);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }, true);
        }
    }
}