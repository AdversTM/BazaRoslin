namespace BazaRoslin.Model.Impl {
    public class Offer : IOffer {

        public int Id { get; set; }
        public int PlantId { get; set; }
        public IShop Shop { get; set; }
        public int Availability { get; set; }
        public decimal Price { get; set; }

        public string PriceText => $"{Price:F}";
        
        public string ToDisplay => $"{Shop.Name} ({Price:F} zł) [{Availability} szt.]";

        public Offer(int id, int plantId, IShop shop, int availability, decimal price) {
            Id = id;
            PlantId = plantId;
            Shop = shop;
            Availability = availability;
            Price = price;
        }

        public int CompareTo(IOffer other) => (Availability > 0 ? Price : decimal.MaxValue)
            .CompareTo(other.Availability > 0 ? other.Price : decimal.MaxValue);
    }
}