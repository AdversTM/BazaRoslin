namespace BazaRoslin.Model.Impl {
    public class Plant : IPlant, IDisplayable {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int WateringFrequency { get; set; }
        public string Fertilization { get; set; }
        public string Size { get; set; }
        public string VegetationStart { get; set; }
        public string VegetationEnd { get; set; }
        public int Temperature { get; set; }
        public ICategory Category { get; set; }
        
        public string ToDisplay => Name;

        public Plant(int id, string name, string image, int wateringFrequency, string fertilization, string size,
            string vegetationStart, string vegetationEnd, int temperature, ICategory category) {
            Id = id;
            Name = name;
            Image = image;
            WateringFrequency = wateringFrequency;
            Fertilization = fertilization;
            Size = size;
            VegetationStart = vegetationStart;
            VegetationEnd = vegetationEnd;
            Temperature = temperature;
            Category = category;
        }

        public override bool Equals(object? obj) {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is not Plant o) return false;
            return Id == o.Id;
        }

        public override string ToString() {
            return $"{nameof(Plant)}({nameof(Id)}={Id}, {nameof(Name)}={Name}, {nameof(Image)}={Image}, " +
                   $"{nameof(WateringFrequency)}={WateringFrequency}, {nameof(Fertilization)}={Fertilization}, " +
                   $"{nameof(Size)}={Size}, {nameof(VegetationStart)}={VegetationStart}, " +
                   $"{nameof(VegetationEnd)}={VegetationEnd}, {nameof(Temperature)}={Temperature}, " +
                   $"{nameof(Category)}={Category})";
        }
    }
}