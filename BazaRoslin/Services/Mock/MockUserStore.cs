using System.Threading.Tasks;
using BazaRoslin.Model;
using BazaRoslin.Model.Impl;

namespace BazaRoslin.Services.Mock {
    public class MockUserStore : IUserStore {
        public async Task<IUser?> GetUser(string login) {
            return login == "test" ? new User(1, "test", "098f6bcd4621d373cade4e832627b4f6", "Jan", "Kowalski") : null;
        }
    }
}