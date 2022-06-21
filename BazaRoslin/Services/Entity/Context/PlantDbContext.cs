using System;
using BazaRoslin.Model.Impl;
using BazaRoslin.Services.Entity.Base;
using BazaRoslin.Util;
using Microsoft.EntityFrameworkCore;

namespace BazaRoslin.Services.Entity.Context {
    public class PlantDbContext : BaseDbContext {

        [ThreadStatic] private static PlantDbContext? _current;

        protected new static PlantDbContext Current() =>
            _current ??= new PlantDbContext(Configuration.ConnectionString);

        protected internal DbSet<Category> Categories { get; set; } = null!;
        protected internal DbSet<Shop> Shops { get; set; } = null!;
        protected internal DbSet<Plant> Plants { get; set; } = null!;
        protected internal DbSet<Offer> Offers { get; set; } = null!;
        protected internal DbSet<PlantCategory> PlantCategories { get; set; } = null!;
        protected internal DbSet<UserPlant> UserPlants { get; set; } = null!;
        protected internal DbSet<OfferRating> OfferRatings { get; set; } = null!;
        protected internal DbSet<OfferFollow> OfferFollows { get; set; } = null!;

        public PlantDbContext(string connectionString) : base(connectionString) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Offer>()
                .HasOne(o => o.Shop)
                .WithMany()
                .HasForeignKey(o => o.ShopId);
            modelBuilder.Entity<Offer>()
                .HasOne(o => o.Plant)
                .WithMany()
                .HasForeignKey(o => o.PlantId);

            modelBuilder.Entity<PlantCategory>()
                .HasKey(pc => new { pc.PlantId, pc.CategoryId });
            modelBuilder.Entity<PlantCategory>()
                .HasOne(pc => pc.Plant)
                .WithMany(p => p.PlantCategories)
                .HasForeignKey(pc => pc.PlantId);
            modelBuilder.Entity<PlantCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.PlantCategories)
                .HasForeignKey(pc => pc.CategoryId);

            modelBuilder.Entity<UserPlant>()
                .HasKey(up => new { up.UserId, up.PlantId });
            modelBuilder.Entity<UserPlant>()
                .HasOne(up => up.User)
                .WithMany()
                .HasForeignKey(up => up.UserId);
            modelBuilder.Entity<UserPlant>()
                .HasOne(up => up.Plant)
                .WithMany()
                .HasForeignKey(up => up.PlantId);

            modelBuilder.Entity<OfferRating>()
                .HasKey(or => new { or.OfferId, or.UserId });
            
            modelBuilder.Entity<OfferFollow>()
                .HasKey(of => new { of.OfferId, of.UserId });
            modelBuilder.Entity<OfferFollow>()
                .HasOne(of => of.Offer)
                .WithMany()
                .HasForeignKey(of => of.OfferId);
        }
    }
}