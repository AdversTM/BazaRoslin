using System.ComponentModel.DataAnnotations.Schema;

namespace BazaRoslin.Model.Impl {
    [Table("lista_życzeń")]
    public class OfferFollow : IOfferFollow {
        [Column("id_oferta")] public int OfferId { get; set; }
        [Column("id_użytkownik")] public int UserId { get; set; }

        public Offer Offer { get; set; } = null!;

        [NotMapped]
        IOffer IOfferFollow.Offer {
            get => Offer;
            set => Offer = (Offer)value;
        }

        [NotMapped] public string ToDisplay => $"{Offer.Plant.Name} ({Offer.Shop.Name})";

        public OfferFollow(int offerId, int userId) {
            OfferId = offerId;
            UserId = userId;
        }

        public override bool Equals(object? obj) {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is not IOfferFollow o) return false;
            return OfferId == o.OfferId && UserId == o.UserId;
        }
    }
}