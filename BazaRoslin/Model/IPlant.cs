using System.Collections.Generic;

namespace BazaRoslin.Model {
    public interface IPlant : IDisplayable {
        int Id { get; set; }
        string Name { get; set; }
        byte[] Image { get; set; }
        string WateringFrequency { get; set; }
        string Fertilization { get; set; }
        int Size { get; set; }
        string VegetationStart { get; set; }
        string VegetationEnd { get; set; }
        string Insolation { get; set; }
        int Temperature { get; set; }
        
        List<IPlantCategory> PlantCategories { get; set; }
        string Categories { get; }
        List<string> CategoriesList { get; }
    }
}