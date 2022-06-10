using System.Collections.Generic;
using System.Threading.Tasks;
using BazaRoslin.Model;

namespace BazaRoslin.Services {
    public interface IPlantStore {
        Task<ICategory?> GetCategory(int id);
        Task<IShop?> GetShop(int id);
        Task<List<IOffer>> GetOffers(int plantId);
        Task<List<IPlant>> GetPlants(int userId);
        Task<List<ICategory>> GetCategories();
        Task<List<IShop>> GetShops();
        Task<List<IPlant>> GetPlants();
        Task DeletePlant(int plantId, int userId);
    }
}