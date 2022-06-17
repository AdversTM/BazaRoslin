using System.ComponentModel.DataAnnotations.Schema;

namespace BazaRoslin.Model.Impl {
    [Table("posiadane_rośliny")]
    public class UserPlant : IUserPlant {
        [Column("id_użytkownik")] public int UserId { get; set; }
        [Column("id_roślina")] public int PlantId { get; set; }
        public User User { get; set; } = null!;
        public Plant Plant { get; set; } = null!;

        [NotMapped]
        IUser IUserPlant.User {
            get => User;
            set => User = (User)value;
        }

        [NotMapped]
        IPlant IUserPlant.Plant {
            get => Plant;
            set => Plant = (Plant)value;
        }

        public UserPlant(int userId, int plantId) {
            UserId = userId;
            PlantId = plantId;
        }

        public UserPlant(User user, Plant plant) : this(user.Id, plant.Id) {
            User = user;
            Plant = plant;
        }

        public override bool Equals(object? obj) {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is not IUserPlant o) return false;
            return UserId == o.UserId && PlantId == o.PlantId;
        }
    }
}