using System.ComponentModel.DataAnnotations.Schema;

namespace BazaRoslin.Model.Impl {
    [Table("sklepy")]
    public class Shop : IShop {
        [Column("id_sklep")] public int Id { get; set; }
        [Column("nazwa")] public string Name { get; set; }

        public Shop(int id, string name) {
            Id = id;
            Name = name;
        }

        public override bool Equals(object? obj) {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is not IShop o) return false;
            return Id == o.Id && Name == o.Name;
        }

        public override string ToString() {
            return $"{nameof(Shop)}({nameof(Id)}={Id}, {nameof(Name)}={Name})";
        }
    }
}