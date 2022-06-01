using BazaRoslin.Util;

namespace BazaRoslin.Model {
    public class User : IUser {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public User(string login, string password, string name, string surname) {
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
            if (obj is not Category o) return false;
            return Id == o.Id && Name == o.Name;
        }

        public override string ToString() {
            return $"{nameof(User)}({nameof(Id)}={Id}, {nameof(Login)}={Login}, {nameof(Password)}={Password}, " +
                   $"{nameof(Name)}={Name}, {nameof(Surname)}={Surname})";
        }
    }
}