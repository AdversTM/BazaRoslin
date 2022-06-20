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
        public Task<ICategory?> GetCategory(int id) => UseContext(ctx =>
            (ICategory?)ctx.Categories.SingleOrDefault(c => c.Id == id));

        public Task<IShop?> GetShop(int id) => UseContext(ctx =>
            (IShop?)ctx.Shops.SingleOrDefault(s => s.Id == id));

        public Task<List<IOffer>> GetOffers(int plantId) => UseContext(ctx =>
            ctx.Offers
                .Where(o => o.PlantId == plantId)
                .Include(o => o.Shop)
                .ToList<IOffer>());

        public Task<List<IPlant>> GetPlants(int userId) => UseContext(ctx => {
            return ctx.UserPlants
                .AsNoTracking()
                .Where(up => up.UserId == userId)
                .Include(up => up.Plant)
                .ThenInclude(p => p.PlantCategories)
                .ThenInclude(pc => pc.Category)
                .Select(up => up.Plant)
                .ToList<IPlant>();
        });

        public Task<List<ICategory>> GetCategories() => UseContext(ctx =>
            ctx.Categories.ToList<ICategory>());

        public Task<List<IPlantCategory>> GetPlantCategories() => UseContext(ctx =>
            ctx.PlantCategories.ToList<IPlantCategory>());

        public Task<List<IShop>> GetShops() => UseContext(ctx =>
            ctx.Shops.ToList<IShop>());

        public Task<List<IPlant>> GetPlants() => UseContext(ctx =>
            ctx.Plants
                .Include(p => p.PlantCategories)
                .ThenInclude(pc => pc.Category)
                .ToList<IPlant>());

        public Task AddUserPlant(int userId, int plantId) => UseContext(ctx => {
            var up = new UserPlant(userId, plantId);
            if (ctx.UserPlants.Any(e => e.Equals(up))) return;
            var e = ctx.Entry(up);
            if (e.State == EntityState.Added) return;
            
            var local = ctx.UserPlants.Local.FirstOrDefault(en => en.Equals(up));
            if (local != null) ctx.Entry(local).State = EntityState.Detached;
            e.State = EntityState.Added;
            ctx.SaveChanges();
        });

        public Task DeleteUserPlant(int userId, int plantId) => UseContext(ctx => {
            var up = new UserPlant(userId, plantId);
            ctx.Entry(up).State = EntityState.Deleted;
            ctx.SaveChanges();
            ctx.Database.ExecuteSqlInterpolated($"DELETE FROM `posiadane_rośliny` WHERE `id_użytkownik`={userId} AND `id_roślina`={plantId}");
        });

        public async Task<IOfferRating> GetRating(int offerId, int userId) =>
            await UseContext(ctx =>
                ctx.OfferRatings.SingleOrDefault<IOfferRating>(or => or.OfferId == offerId && or.UserId == userId))
            ?? new OfferRating(offerId, userId, 0);

        public Task SetRating(IOfferRating offerRating) => UseContext(ctx => {
            var or = (OfferRating)offerRating;
            if (ctx.OfferRatings.Any(e => e.OfferId == offerRating.OfferId && e.UserId == offerRating.UserId))
                ctx.Attach(or).State = EntityState.Modified;
            else ctx.Add(or);
            ctx.SaveChanges();
        });

        public Task<List<IOfferFollow>> GetOfferFollows(int userId) => UseContext(ctx =>
            ctx.OfferFollows
                .Where(of => of.UserId == userId)
                .Include(of => of.Offer)
                .ThenInclude(o => o.Shop)
                .Include(of => of.Offer)
                .ThenInclude(o => o.Plant)
                .ThenInclude(p => p.PlantCategories)
                .ThenInclude(pc => pc.Category)
                .ToList<IOfferFollow>());

        public Task<bool> IsFollow(int offerId, int userId) => UseContext(ctx =>
            ctx.OfferFollows.Any(of => of.OfferId == offerId && of.UserId == userId));

        public Task SetFollow(int offerId, int userId, bool isFollow) => UseContext(ctx => {
            var of = new OfferFollow(offerId, userId);
            var exists = ctx.OfferFollows.Any(e => e.OfferId == offerId && e.UserId == userId);
            if (isFollow) {
                if (exists) return;
                var e = ctx.Entry(of);
                if (e.State == EntityState.Added) return;
                if (e.State == EntityState.Unchanged)
                    e.State = EntityState.Modified;
                else ctx.Add(of);
                ctx.SaveChanges();
            } else {
                if (!exists) return;
                var e = ctx.Entry(of);
                if (e.State is EntityState.Detached or EntityState.Deleted) return;
                e.State = EntityState.Deleted;
                ctx.SaveChanges();
            }
        });

        public Task<IOfferFollow> NewOfferFollow(int offerId, int userId) => UseContext(ctx => {
            var o = ctx.Offers
                .Include(o => o.Shop)
                .Include(o => o.Plant)
                .ThenInclude(p => p.PlantCategories)
                .ThenInclude(pc => pc.Category)
                .Single(o => o.Id == offerId)!;
            return (IOfferFollow)new OfferFollow(offerId, userId) { Offer = o };
        });
    }
}