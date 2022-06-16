using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BazaRoslin.Model;
using BazaRoslin.Model.Impl;
using BazaRoslin.Services.Entity.Base;
using BazaRoslin.Services.Entity.Context;
using Microsoft.EntityFrameworkCore;

namespace BazaRoslin.Services.Entity {
    public class PlantDbRepository : BaseDbRepository<PlantDbContext>, IPlantStore {

        public async Task<ICategory?> GetCategory(int id) {
            return await Context.Categories.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IShop?> GetShop(int id) {
            return await Context.Shops.SingleOrDefaultAsync(s => s.Id == id);
        }

        public Task<List<IOffer>> GetOffers(int plantId) => UseContext(ctx => {
            return ctx.Offers
                .Where(o => o.PlantId == plantId)
                .Include(o => o.Shop)
                .ToList<IOffer>();
        });

        public Task<List<IPlant>> GetPlants(int userId) => UseContext(ctx => {
            var query = from up in ctx.UserPlants
                join plant in Context.Plants
                    on up.PlantId equals plant.Id
                where up.UserId.Equals(userId)
                select plant;
            return query.ToList<IPlant>();
        });

        public Task<List<ICategory>> GetCategories() => UseContext(ctx =>
            ctx.Categories.ToList<ICategory>());

        public Task<List<IPlantCategory>> GetPlantCategories() => UseContext(ctx =>
            ctx.PlantCategories.ToList<IPlantCategory>());

        public Task<List<IShop>> GetShops() => UseContext(ctx =>
            ctx.Shops.ToList<IShop>());

        public Task<List<IPlant>> GetPlants() => UseContext(ctx =>
            ctx.Plants.ToList<IPlant>());

        public Task DeletePlant(int plantId, int userId) => UseContext(ctx => {
            // Remove(Entry(new UserPlant(plantId, userId)));
            ctx.Entry(new UserPlant(plantId, userId)).State = EntityState.Deleted;
            ctx.SaveChanges();
            // Database.ExecuteSqlCommand("Delete DepartmentMasters where DepartmentId = {0}", 8);
        });
    }
}