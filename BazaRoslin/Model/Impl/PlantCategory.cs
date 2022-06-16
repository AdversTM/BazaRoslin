using System.ComponentModel.DataAnnotations.Schema;

namespace BazaRoslin.Model.Impl {
    [Table("kategorie_roślin")]
    public class PlantCategory : IPlantCategory {
        [Column("id_roślina")]
        public int PlantId { get; set; }
        [Column("id_kategoria")]
        public int CategoryId { get; set; }
        public Plant Plant { get; set; } = null!;
        public Category Category { get; set; } = null!;

        [NotMapped]
        IPlant IPlantCategory.Plant {
            get => Plant;
            set => Plant = (Plant)value;
        }

        [NotMapped]
        ICategory IPlantCategory.Category {
            get => Category;
            set => Category = (Category)value;
        }

        public PlantCategory() {
        }
        
        public PlantCategory(int plantId, int categoryId) {
            PlantId = plantId;
            CategoryId = categoryId;
        }

        public PlantCategory(Plant plant, Category category) : this(plant.Id, category.Id) {
            Plant = plant;
            Category = category;
        }
        
        public override bool Equals(object? obj) {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is not IPlantCategory o) return false;
            return PlantId == o.PlantId && CategoryId == o.CategoryId;
        }
    }
}