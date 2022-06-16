using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BazaRoslin.Model.Impl {
    [Table("kategorie")]
    public class Category : ICategory {
        [Column("id_kategoria")] public int Id { get; set; }
        [Column("nazwa")] public string Name { get; set; }

        public List<PlantCategory> PlantCategories { get; set; } = null!;
        
        [NotMapped]
        List<IPlantCategory> ICategory.PlantCategories {
            get => PlantCategories.ToList<IPlantCategory>();
            set => PlantCategories = value.Cast<PlantCategory>().ToList();
        }

        public Category(int id, string name) {
            Id = id;
            Name = name;
        }

        public override bool Equals(object? obj) {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is not ICategory o) return false;
            return Id == o.Id;
        }

        public override string ToString() {
            return $"{nameof(Category)}({nameof(Id)}={Id}, {nameof(Name)}={Name})"; //, {nameof(Plants)}={Plants})";
        }
    }
}