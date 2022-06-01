using BazaRoslin.Model;
using Prism.Events;

namespace BazaRoslin.Event {
    public class UserLoggedEvent : PubSubEvent<IUser> {
    }
}