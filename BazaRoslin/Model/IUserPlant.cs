namespace BazaRoslin.Model {
    public interface IUserPlant {
        int UserId { get; set; }
        int PlantId { get; set; }
        IUser User { get; set; }
        IPlant Plant { get; set; }

    }
}