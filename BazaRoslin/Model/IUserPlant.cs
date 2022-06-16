namespace BazaRoslin.Model {
    public interface IUserPlant {
        int PlantId { get; set; }
        IPlant Plant { get; set; }
        int UserId { get; set; }
        IUser User { get; set; }
    }
}