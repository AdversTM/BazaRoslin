using System;
using BazaRoslin.Model.Impl;
using BazaRoslin.Services.Entity.Base;
using BazaRoslin.Util;
using Microsoft.EntityFrameworkCore;

namespace BazaRoslin.Services.Entity.Context {
    public class UserDbContext : BaseDbContext {

        [ThreadStatic] private static UserDbContext? _current;

        protected new static UserDbContext Current() =>
            _current ??= new UserDbContext(Configuration.ConnectionString);

        protected internal DbSet<User> Users { get; set; } = null!;

        public UserDbContext(string connectionString) : base(connectionString) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
        }
    }
}