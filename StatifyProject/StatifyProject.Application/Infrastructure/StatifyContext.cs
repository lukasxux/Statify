using Bogus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using StatifyProject.Application.Model;

namespace StatifyProject.Application.Infrastructure
{
    public class StatifyContext : DbContext
    {
        public DbSet<User> Users => Set<User>();


        public StatifyContext(DbContextOptions<StatifyContext> opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Additional config

            // Generic config for all entities
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // ON DELETE RESTRICT instead of ON DELETE CASCADE
                foreach (var key in entityType.GetForeignKeys())
                    key.DeleteBehavior = DeleteBehavior.Restrict;

                foreach (var prop in entityType.GetDeclaredProperties())
                {
                    // Define Guid as alternate key. The database can create a guid fou you.
                    if (prop.Name == "Guid")
                    {
                        modelBuilder.Entity(entityType.ClrType).HasAlternateKey("Guid");
                        prop.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAdd;
                    }
                    // Default MaxLength of string Properties is 255.
                    if (prop.ClrType == typeof(string) && prop.GetMaxLength() is null) prop.SetMaxLength(255);
                    // Seconds with 3 fractional digits.
                    if (prop.ClrType == typeof(DateTime)) prop.SetPrecision(3);
                    if (prop.ClrType == typeof(DateTime?)) prop.SetPrecision(3);
                }
            }
        }

        private void Initialize()
        {
            Randomizer.Seed = new Random(1039);
            var faker = new Faker("de");
            

            var users = new Faker<User>("de").CustomInstantiator(f =>
            {
                var username = f.Hacker.Noun();
                return new User(
                  username: "matrix",
                  email: $"{username}@mail.at",
                  initialPassword: "1111",
                  created_at: new DateTime(2022, 1, 1),
                  favoriteSong: "FavSong",
                  favoriteArtist: "FavArtist")

                { Guid = f.Random.Guid() };
            })
            .Generate(20)
            .GroupBy(e => e.Username).Select(g => g.First())
            .ToList();


        }

        public void Seed()
        {
            Randomizer.Seed = new Random(1037);
            var faker = new Faker("de");
          

            var users = new Faker<User>("de").CustomInstantiator(f =>
            {
                var username = f.Hacker.Noun();
                return new User(
                  username: username,
                  email: $"{username}@mail.at",
                  initialPassword: "1111",
                  created_at: f.Date.Between(new DateTime(2021, 1, 1), new DateTime(2022, 1, 1)),
                  favoriteSong: "FavSong",
                  favoriteArtist: "FavArtist")

                { Guid = f.Random.Guid() };
            })
            .Generate(20)
            .GroupBy(e => e.Username).Select(g => g.First())
            .ToList();

            Users.AddRange(users);
            SaveChanges();

        }

        /// <summary>
        /// Creates the database. Called once at application startup.
        /// </summary>
        public void CreateDatabase(bool isDevelopment)
        {
            if (isDevelopment) { Database.EnsureDeleted(); }
            // EnsureCreated only creates the model if the database does not exist or it has no
            // tables. Returns true if the schema was created.  Returns false if there are
            // existing tables in the database. This avoids initializing multiple times.
            if (Database.EnsureCreated()) { Initialize(); }
            if (isDevelopment) Seed();
        }
    }
}
