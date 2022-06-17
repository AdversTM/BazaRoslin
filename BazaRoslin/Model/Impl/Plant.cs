using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BazaRoslin.Model.Impl {
    [Table("rośliny")]
    public class Plant : IPlant {
        [Column("id")] public int Id { get; set; }
        [Column("nazwa")] public string Name { get; set; }
        [Column("zdjęcie")] public byte[] Image { get; set; }
        [Column("częstość_podlewania")] public string WateringFrequency { get; set; }
        [Column("nawożenie")] public string Fertilization { get; set; }
        [Column("wielkość")] public int Size { get; set; }
        [Column("początek_wegetacji")] public string VegetationStart { get; set; }
        [Column("koniec_wegetacji")] public string VegetationEnd { get; set; }
        [Column("światło")] public string Insolation { get; set; }
        [Column("temperatura_otoczenia")] public int Temperature { get; set; }
        
        public List<PlantCategory> PlantCategories { get; set; } = null!;

        [NotMapped]
        List<IPlantCategory> IPlant.PlantCategories {
            get => PlantCategories.ToList<IPlantCategory>();
            set => PlantCategories = value.Cast<PlantCategory>().ToList();
        }
        [NotMapped] public string ToDisplay => Name;

        public Plant(int id, string name, byte[] image, string wateringFrequency, string fertilization, int size,
            string vegetationStart, string vegetationEnd, string insolation, int temperature) {
            Id = id;
            Name = name;
            Image = image;
            WateringFrequency = wateringFrequency;
            Fertilization = fertilization;
            Size = size;
            VegetationStart = vegetationStart;
            VegetationEnd = vegetationEnd;
            Insolation = insolation;
            Temperature = temperature;
        }

        public override bool Equals(object? obj) {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is not IPlant o) return false;
            return Id == o.Id;
        }

        public override string ToString() {
            return $"{nameof(Plant)}({nameof(Id)}={Id}, {nameof(Name)}={Name}, {nameof(Image)}={Image}, " +
                   $"{nameof(WateringFrequency)}={WateringFrequency}, {nameof(Fertilization)}={Fertilization}, " +
                   $"{nameof(Size)}={Size}, {nameof(VegetationStart)}={VegetationStart}, " +
                   $"{nameof(VegetationEnd)}={VegetationEnd}, {nameof(Insolation)}={Insolation}, " +
                   $"{nameof(Temperature)}={Temperature})";
        }
    }
}