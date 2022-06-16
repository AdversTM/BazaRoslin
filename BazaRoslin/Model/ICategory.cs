﻿using System.Collections.Generic;

namespace BazaRoslin.Model {
    public interface ICategory {
        int Id { get; set; }
        string Name { get; set; }
        // List<IPlant> Plants { get; set; }
        public List<IPlantCategory> PlantCategories { get; set; }
    }
}