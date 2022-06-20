using System.ComponentModel.DataAnnotations.Schema;

namespace BazaRoslin.Model.Impl {
    [Table("oferty")]
    public class Offer : IOffer {
        [Column("id_oferta")] public int Id { get; set; }
        [Column("id_rośliny")] public int PlantId { get; set; }
        [Column("id_sklepu")] public int ShopId { get; set; }
        [Column("dostępność")] public int Availability { get; set; }
        [Column("cena")] public decimal Price { get; set; }

        public Plant Plant { get; set; } = null!;
        public Shop Shop { get; set; } = null!;
        
        [NotMapped]
        IPlant IOffer.Plant {
            get => Plant;
            set => Plant = (Plant)value;
        }

        [NotMapped]
        IShop IOffer.Shop {
            get => Shop;
            set => Shop = (Shop)value;
        }

        [NotMapped] public string ToDisplay => $"{Shop.Name} ({Price:F} zł) [{Availability} szt.]";

        public Offer(int id, int plantId, int shopId, int availability, decimal price) {
            Id = id;
            PlantId = plantId;
            ShopId = shopId;
            Availability = availability;
            Price = price;
        }
        
        public override bool Equals(object? obj) {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is not IOffer o) return false;
            return Id == o.Id;
        }

        public int CompareTo(IOffer other) => (Availability > 0 ? Price : decimal.MaxValue)
            .CompareTo(other.Availability > 0 ? other.Price : decimal.MaxValue);
    }
}