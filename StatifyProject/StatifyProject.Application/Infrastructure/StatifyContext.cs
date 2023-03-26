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
        public DbSet<Song> Songs => Set<Song>();
        public DbSet<Artist> Artists => Set<Artist>();

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
            var artists = new Artist[]
            {
                new Artist(Name: "Tom Odell"),
                new Artist(Name: "Luca Racz")
            };
            Artists.AddRange(artists);
            SaveChanges();

            var songs = new Song[]
           {
                new Song(Title: "Another Love", Artist: artists[0], Created_at: DateTime.UtcNow, Length: TimeSpan.FromSeconds(3*60+10),
                    ImageUrl: "https://media.hitparade.ch/cover/big/tom_odell-another_love_s_3.jpg",
                    Link: "https://www.youtube.com/watch?v=MwpMEbgC7DA") {Guid = faker.Random.Guid()},
                                new Song(Title: "Ratsch", Artist: artists[1], Created_at: DateTime.UtcNow, Length: TimeSpan.FromSeconds(3*60+10),
                    ImageUrl: "https://media.istockphoto.com/id/1216562320/de/foto/h%C3%A4nde-rei%C3%9Fen-ein-st%C3%BCck-papier-auf-wei%C3%9F.jpg?s=612x612&w=0&k=20&c=6ju_bwJAlq1ApxxEC-xxg-LIsA6uD_nQr-MyUXM86Sg=",
                    Link: "https://www.youtube.com/watch?v=g5phW2oa7Uw&ab_channel=SiimonSounds") {Guid = faker.Random.Guid()},
           };
            Songs.AddRange(songs);
            SaveChanges();

            var users = new Faker<User>("de").CustomInstantiator(f =>
            {
                var username = f.Hacker.Noun();
                return new User(
                  username: "matrix",
                  email: $"{username}@mail.at",
                  initialPassword: "1111",
                  created_at: new DateTime(2022, 1, 1),
                  favoriteSong: songs[1],
                  favoriteArtist: artists[1])

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
            var artists = new Artist[]
            {
                new Artist(Name: "Ayliva Feat. Mero"),
                new Artist(Name: "David Guetta & Bebe Rexha"),
                new Artist(Name: "Sam Smith & Kim Petras"),
                new Artist(Name: "Raye Feat. 070 Shake"),

            };
            Artists.AddRange(artists);
            SaveChanges();

            var songs = new Song[]
            {
                new Song(Title: "Sie Weiß", Artist: artists[0], Created_at: DateTime.UtcNow, Length: TimeSpan.FromSeconds(3*60+10),
                    ImageUrl: "https://i1.sndcdn.com/artworks-sn0gP4CS8xCr61su-PE2xrQ-t500x500.jpg",
                    Link: "https://www.youtube.com/watch?v=69rX_1z-p2g") {Guid = faker.Random.Guid()},
                new Song(Title: "I'm Good (Blue)", Artist: artists[1], Created_at: DateTime.UtcNow, Length: TimeSpan.FromSeconds(3*60+10), 
                    ImageUrl: "https://i.ytimg.com/vi/90RLzVUuXe4/maxresdefault.jpg",
                    Link: "https://www.youtube.com/watch?v=90RLzVUuXe4") {Guid = faker.Random.Guid()},
                new Song(Title: "Unholy", Artist: artists[2], Created_at: DateTime.UtcNow, Length: TimeSpan.FromSeconds(3*60+10),
                    ImageUrl: "https://www1.wdr.de/radio/1live/musik/neu-fuer-den-sektor/sam-smith-kim-petras-100~_v-gseapremiumxl.jpg",
                    Link: "https://www.youtube.com/watch?v=Uq9gPaIzbe8") {Guid = faker.Random.Guid()},
                new Song(Title: "Escapism.", Artist: artists[3], Created_at: DateTime.UtcNow, Length: TimeSpan.FromSeconds(3*60+10), 
                    ImageUrl: "https://i.ytimg.com/vi/0EBw-CWc4Uw/maxresdefault.jpg",
                    Link: "https://www.youtube.com/watch?v=Dll6VJ2C7wo") {Guid = faker.Random.Guid()}
            };
            Songs.AddRange(songs);
            SaveChanges();

            var users = new Faker<User>("de").CustomInstantiator(f =>
            {
                var username = f.Hacker.Noun();
                return new User(
                  username: username,
                  email: $"{username}@mail.at",
                  initialPassword: "1111",
                  created_at: f.Date.Between(new DateTime(2021, 1, 1), new DateTime(2022, 1, 1)),
                  favoriteSong: f.Random.ListItem(songs),
                  favoriteArtist: f.Random.ListItem(artists))

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
