using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BazaRoslin.Model;
using BazaRoslin.Model.Impl;
using BazaRoslin.Util;

namespace BazaRoslin.Services.Mock {
    public class MockPlantStore : IPlantStore {

        private readonly List<ICategory> _categories = new() {
            new Category(1, "Testowa"), new Category(2, "Kategoria")
        };

        private readonly List<IShop> _shops = new() {
            new Shop(1, "Biedronka"), new Shop(2, "Żabka")
        };

        private readonly List<IPlant> _plants;

        private List<IOffer> _offers = null!;

        public MockPlantStore() {
            var c1 = (Category)_categories.Find(c => c.Id == 1)!;
            var c2 = (Category)_categories.Find(c => c.Id == 2)!;
            _plants = new() {
                new Plant(1, "Roslina 1", Image.GetBytes("../../../resources/kwiat1.png"), "obficie",
                        "123", 123, "styczeń", "czerwiec", "mocno", 23)
                    .Also(p => ((IPlant)p).PlantCategories = new List<IPlantCategory> { new PlantCategory(p, c1) }),
                new Plant(2, "Roslina 2", Image.GetBytes("../../../resources/kwiat2.png"), "regularnie",
                        "321", 321, "lipiec", "grudzień", "słabo", 34)
                    .Also(p => ((IPlant)p).PlantCategories = new List<IPlantCategory> { new PlantCategory(p, c2) }),
            };

            Load();
        }

        private async void Load() {
            var s1 = (await GetShop(1))!;
            var s2 = (await GetShop(2))!;
            _offers = new() {
                new Offer(1, 1, 1, 53, new(34.56)).Also(o => ((IOffer)o).Shop = s1),
                new Offer(2, 2, 1, 14, new(52.99)).Also(o => ((IOffer)o).Shop = s1),
                new Offer(3, 1, 2, 12, new(20.00)).Also(o => ((IOffer)o).Shop = s2),
                new Offer(4, 2, 2, 0, new(45.99)).Also(o => ((IOffer)o).Shop = s2),
            };
        }

        public Task<ICategory?> GetCategory(int id) =>
            Task.FromResult((ICategory?)_categories.FirstOrDefault(c => c.Id == id));

        public Task<IShop?> GetShop(int id) =>
            Task.FromResult((IShop?)_shops.FirstOrDefault(s => s.Id == id));

        public Task<List<IOffer>> GetOffers(int plantId) =>
            Task.FromResult(_offers.FindAll(o => o.PlantId == plantId));

        public Task<List<IPlant>> GetPlants(int userId) =>
            Task.FromResult<List<IPlant>>(new() { _plants.Find(p => p.Id == 1) });

        public Task<List<ICategory>> GetCategories() => Task.FromResult(_categories);

        public Task<List<IShop>> GetShops() => Task.FromResult(_shops);

        public Task<List<IPlant>> GetPlants() => Task.FromResult(_plants);

        public Task AddUserPlant(int userId, int plantId) => Task.CompletedTask;

        public Task DeleteUserPlant(int userId, int plantId) => Task.CompletedTask;

        public Task<IOfferRating> GetRating(int offerId, int userId) =>
            Task.FromResult<IOfferRating>(new OfferRating(offerId, userId, 1));

        public Task SetRating(IOfferRating offerRating) => Task.CompletedTask;

        public Task<List<IOfferFollow>> GetOfferFollows(int userId) =>
            Task.FromResult<List<IOfferFollow>>(new());

        public Task<bool> IsFollow(int offerId, int userId) => Task.FromResult(offerId % 2 == 0);

        public Task SetFollow(int offerId, int userId, bool isFollow) => Task.CompletedTask;

        public Task<IOfferFollow> NewOfferFollow(int offerId, int userId) => Task.FromResult<IOfferFollow>(
            new OfferFollow(offerId, userId) { Offer = (Offer)_offers.Find(o => o.Id == offerId) }
        );
    }
}