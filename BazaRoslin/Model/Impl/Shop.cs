namespace BazaRoslin.Model.Impl {
    public class Shop : IShop {
        public int Id { get; set; }
        public string Name { get; set; }

        public Shop(int id, string name) {
            Id = id;
            Name = name;
        }

        public override bool Equals(object? obj) {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is not Shop o) return false;
            return Id == o.Id && Name == o.Name;
        }

        public override string ToString() {
            return $"{nameof(Shop)}({nameof(Id)}={Id}, {nameof(Name)}={Name})";
        }
    }
}