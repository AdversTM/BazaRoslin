using BazaRoslin.Util;

namespace BazaRoslin.Model {
    public class User : IUser {
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
    }
}