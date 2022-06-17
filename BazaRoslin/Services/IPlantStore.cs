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
        Task AddUserPlant(int userId, int plantId);
        Task DeleteUserPlant(int userId, int plantId);
        Task<IOfferRating> GetRating(int offerId, int userId);
        Task SetRating(IOfferRating offerRating);
        Task<List<IOfferFollow>> GetOfferFollows(int userId);
        Task<bool> IsFollow(int offerId, int userId);
        Task SetFollow(int offerId, int userId, bool isFollow);
        Task<IOfferFollow> NewOfferFollow(int offerId, int userId);
    }
}