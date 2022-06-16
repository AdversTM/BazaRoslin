using System.Collections.Generic;

namespace BazaRoslin.Model {
    public interface IPlant {
        int Id { get; set; }
        string Name { get; set; }
        byte[] Image { get; set; }
        string WateringFrequency { get; set; }
        string Fertilization { get; set; }
        int Size { get; set; }
        string VegetationStart { get; set; }
        string VegetationEnd { get; set; }
        int Temperature { get; set; }
     
        public List<IPlantCategory> PlantCategories { get; set; }
    }
}