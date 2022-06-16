using System;
using BazaRoslin.Model.Impl;
using BazaRoslin.Services.Entity.Base;
using BazaRoslin.Util;
using Microsoft.EntityFrameworkCore;

namespace BazaRoslin.Services.Entity.Context {
    public class PlantDbContext : BaseDbContext {

        [ThreadStatic] private static PlantDbContext? _current;
        protected new static PlantDbContext Current() => _current ??= new PlantDbContext(Configuration.ConnectionString);

        protected internal DbSet<Category> Categories { get; set; } = null!;
        protected internal DbSet<Shop> Shops { get; set; } = null!;
        protected internal DbSet<Plant> Plants { get; set; } = null!;
        public DbSet<Offer> Offers { get; set; } = null!;
        protected internal DbSet<PlantCategory> PlantCategories { get; set; } = null!;
        protected internal DbSet<UserPlant> UserPlants { get; set; } = null!;

        public PlantDbContext(string connectionString) : base(connectionString) {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Offer>()
                .HasOne(o => o.Shop)
                .WithMany()
                .HasForeignKey(o => o.ShopId);

            modelBuilder.Entity<PlantCategory>()
                .HasKey(t => new { t.PlantId, t.CategoryId });
            modelBuilder.Entity<PlantCategory>()
                .HasOne(pc => pc.Plant)
                .WithMany(p => p.PlantCategories)
                .HasForeignKey(pc => pc.PlantId);
            modelBuilder.Entity<PlantCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(t => t.PlantCategories)
                .HasForeignKey(pt => pt.CategoryId);

            modelBuilder.Entity<UserPlant>()
                .HasKey(t => new { t.UserId, t.PlantId });
            modelBuilder.Entity<UserPlant>()
                .HasOne(pc => pc.User)
                .WithMany()
                .HasForeignKey(pc => pc.UserId);
            modelBuilder.Entity<UserPlant>()
                .HasOne(pc => pc.Plant)
                .WithMany()
                .HasForeignKey(pt => pt.PlantId);
        } 
    }
}