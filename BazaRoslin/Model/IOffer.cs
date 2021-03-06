using System;

namespace BazaRoslin.Model {
    public interface IOffer : IDisplayable, IComparable<IOffer> {
        int Id { get; set; }
        int PlantId { get; set; }
        int ShopId { get; set; }
        int Availability { get; set; }
        decimal Price { get; set; }
        
        IPlant Plant { get; set; }
        IShop Shop { get; set; }
    }
}