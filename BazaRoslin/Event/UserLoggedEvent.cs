using BazaRoslin.Model;
using Prism.Events;

namespace BazaRoslin.Event {
    public class UserLoggedEvent : PubSubEvent<IUser> {
        public IUser User = null!;
        
        public UserLoggedEvent() {
        }
        
        public UserLoggedEvent(IUser user) {
            User = user;
        }
    }
}