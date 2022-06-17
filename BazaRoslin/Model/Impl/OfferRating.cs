using System.ComponentModel.DataAnnotations.Schema;

namespace BazaRoslin.Model.Impl {
    [Table("oceny")]
    public class OfferRating : IOfferRating {
        [Column("id_oferta")] public int OfferId { get; set; }
        [Column("id_użytkownik")] public int UserId { get; set; }
        [Column("ocena")] public decimal Rating { get; set; }

        public OfferRating(int offerId, int userId, decimal rating) {
            OfferId = offerId;
            UserId = userId;
            Rating = rating;
        }
        
        public override bool Equals(object? obj) {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is not IOfferRating o) return false;
            return OfferId == o.OfferId && UserId == o.UserId && Rating == o.Rating;
        }
    }
}