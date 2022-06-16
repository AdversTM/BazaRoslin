using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Storage;

namespace BazaRoslin.Services.Entity.Base {
    public abstract class BaseDbContext : DbContext {

        public static BaseDbContext Current() => throw new InvalidOperationException();

        private readonly string _connectionString;

        protected BaseDbContext(string connectionString) {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            if (options.IsConfigured) return;

            options.UseMySql(_connectionString, builder =>
                    builder.ServerVersion(ServerVersion.AutoDetect(_connectionString)))
                .UseLoggerFactory(new LoggerFactory())
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
    }
}