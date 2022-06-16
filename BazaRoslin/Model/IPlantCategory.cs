namespace BazaRoslin.Model {
    public interface IPlantCategory {
        int PlantId { get; set; }
        IPlant Plant { get; set; }
        int CategoryId { get; set; }
        ICategory Category { get; set; }
    }
}