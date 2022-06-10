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
            _plants = new() {
                new Plant(1, "Roslina 1", Image.ToBase64("../../../resources/kwiat1.png"), 1,
                    "123", "duży", "styczeń", "czerwiec", 23,
                    _categories.Find(c => c.Id == 1)),
                new Plant(2, "Roslina 2", Image.ToBase64("../../../resources/kwiat2.png"), 2,
                    "321", "mały", "lipiec", "grudzień", 34,
                    _categories.Find(c => c.Id == 2))
            };

            Load();
        }

        private async void Load() {
            _offers = new() {
                new Offer(1, 1, (await GetShop(1))!, 53, new(34.56)),
                new Offer(2, 2, (await GetShop(1))!, 14, new(52.99)),
                new Offer(3, 1, (await GetShop(2))!, 12, new(20.00)),
                new Offer(4, 2, (await GetShop(2))!, 0, new(45.99)),
            };
        }

        public async Task<ICategory?> GetCategory(int id) => _categories.FirstOrDefault(c => c.Id == id);
        
        public async Task<IShop?> GetShop(int id) => _shops.FirstOrDefault(s => s.Id == id);

        public async Task<List<IOffer>> GetOffers(int plantId) => _offers.FindAll(o => o.PlantId == plantId);
        
        public async Task<List<IPlant>> GetPlants(int userId) => new() { _plants.Find(p => p.Id == 1) };

        public async Task<List<ICategory>> GetCategories() => _categories;

        public async Task<List<IShop>> GetShops() => _shops;

        public async Task<List<IPlant>> GetPlants() => _plants;
        public async Task DeletePlant(int plantId, int userId) {
            throw new System.NotImplementedException();
        }
    }
}