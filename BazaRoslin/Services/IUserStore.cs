using System.Threading.Tasks;
using BazaRoslin.Model;

namespace BazaRoslin.Services {

    public interface IUserStore {
        Task<IUser?> GetUser(string login);
    }
}