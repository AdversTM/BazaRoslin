using System.ComponentModel.DataAnnotations.Schema;

namespace BazaRoslin.Model.Impl {
    [Table("posiadane_rośliny")]
    public class UserPlant : IUserPlant {
        [Column("id_roślina")] public int PlantId { get; set; }
        [Column("id_użytkownik")] public int UserId { get; set; }
        public Plant Plant { get; set; }
        public User User { get; set; }

        [NotMapped]
        IPlant IUserPlant.Plant {
            get => Plant;
            set => Plant = (Plant)value;
        }

        [NotMapped]
        IUser IUserPlant.User {
            get => User;
            set => User = (User)value;
        }

        public UserPlant(int plantId, int userId) {
            PlantId = plantId;
            UserId = userId;
        }

        public UserPlant(Plant plant, User user) : this(plant.Id, user.Id) {
            Plant = plant;
            User = user;
        }

        public override bool Equals(object? obj) {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is not IUserPlant o) return false;
            return PlantId == o.PlantId && UserId == o.UserId;
        }
    }
}