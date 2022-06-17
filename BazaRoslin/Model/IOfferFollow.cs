namespace BazaRoslin.Model {
    public interface IOfferFollow : IDisplayable {
        int OfferId { get; set; }
        int UserId { get; set; }
        IOffer Offer { get; set; }
    }
}