﻿namespace BazaRoslin.Model {
    public interface IUser {
        int Id { get; set; }
        string Login { get; set; }
        string Password { get; set; }
        string Name { get; set; }
        string Surname { get; set; }

        bool CheckPassword(string password);
    }
}