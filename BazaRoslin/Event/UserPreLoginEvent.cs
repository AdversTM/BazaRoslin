using BazaRoslin.Model;
using Prism.Events;

namespace BazaRoslin.Event {
    public class UserPreLoginEvent : PubSubEvent<LoginArgs> {
    }

    public class LoginArgs {
        public readonly IUser User;
        public readonly LoginPhase Phase;

        public LoginArgs(IUser user, LoginPhase phase) {
            User = user;
            Phase = phase;
        }
    }
    
    public enum LoginPhase {
        Request, Response, Success
    }
}