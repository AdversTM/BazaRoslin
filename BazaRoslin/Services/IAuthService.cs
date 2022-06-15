using BazaRoslin.Model;

namespace BazaRoslin.Services {
    public interface IAuthService {
        public IUser? CurrentUser { get; }
        public IUser LoggedUser { get; }
        public bool IsLogged { get; }
    }
}