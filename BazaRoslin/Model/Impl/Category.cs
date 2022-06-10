namespace BazaRoslin.Model.Impl {
    public class Category : ICategory {
        public int Id { get; set; }
        public string Name { get; set; }
        // public List<IPlant> Plants { get; set; } = new();

        public Category(int id, string name) {
            Id = id;
            Name = name;
        }

        public override bool Equals(object? obj) {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is not Category o) return false;
            return Id == o.Id;
        }

        public override string ToString() {
            return $"{nameof(Category)}({nameof(Id)}={Id}, {nameof(Name)}={Name})";//, {nameof(Plants)}={Plants})";
        }
    }
}