namespace BazaRoslin.Model {
    public interface IOfferRating {
        int OfferId { get; set; }
        int UserId { get; set; }
        decimal Rating { get; set; }
    }
}