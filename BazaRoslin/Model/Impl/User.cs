using System.ComponentModel.DataAnnotations.Schema;
using BazaRoslin.Util;

namespace BazaRoslin.Model.Impl {
    [Table("użytkownicy")]
    public class User : IUser {
        [Column("id_użytkownik")] public int Id { get; set; }
        [Column("login")] public string Login { get; set; }
        [Column("hasło")] public string Password { get; set; }
        [Column("imię")] public string Name { get; set; }
        [Column("nazwisko")] public string Surname { get; set; }

        public User(int id, string login, string password, string name, string surname) {
            Id = id;
            Login = login;
            Password = password;
            Name = name;
            Surname = surname;
        }

        public bool CheckPassword(string password) {
            return Hash.CreateMD5(password) == Password;
        }

        public override bool Equals(object? obj) {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is not IUser o) return false;
            return Id == o.Id && Login == o.Login;
        }

        public override string ToString() {
            return $"{nameof(User)}({nameof(Id)}={Id}, {nameof(Login)}={Login}, {nameof(Password)}={Password}, " +
                   $"{nameof(Name)}={Name}, {nameof(Surname)}={Surname})";
        }
    }
}