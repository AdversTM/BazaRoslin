using System.Linq;
using System.Threading.Tasks;
using BazaRoslin.Model;
using BazaRoslin.Services.Entity.Base;
using BazaRoslin.Services.Entity.Context;

namespace BazaRoslin.Services.Entity {
    public class UserDbRepository : BaseDbRepository<UserDbContext>, IUserStore {
        public Task<IUser?> GetUser(string login) => UseContext(ctx =>
            (IUser?)ctx.Users.SingleOrDefault(u => u.Login == login));
    }
}