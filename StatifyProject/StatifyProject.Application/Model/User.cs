namespace StatifyProject.Application.Model
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    [Index(nameof(Id), IsUnique = true)]
    [Index(nameof(Username), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public User(string username, string email, string initialPassword, DateTime created_at, string bio, UserRole role, string? favoriteSong = null, string? favoriteArtist = null)
        {
            Username = username;
            Email = email;
            SetPassword(initialPassword);
            Created_at = created_at;
            FavoriteSong = favoriteSong;
            FavoriteArtist = favoriteArtist;
            Bio = bio;
            Role = role;
            
        }

#pragma warning disable CS8618
        protected User() { }
#pragma warning restore CS8618


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid Guid { get; set; }

        [MaxLength(20)]
        public string Username { get; set; }
        [MaxLength(40)]
        public string Email { get; set; }
        [MaxLength(50)]
 
        public DateTime Created_at { get; set; }
        public String? FavoriteSong { get; set; }
        public String? FavoriteArtist { get; set; }
        public String? Bio { get; set; }
        public UserRole Role { get; set; }
        public string Salt { get; set; }
        public string PasswordHash { get; set; }

        /// <summary>
        /// Generates a random number with the given length of bits.
        /// </summary>
        /// <param name="length">Default: 128 bits (16 Bytes)</param>
        /// <returns>A base64 encoded string from the byte array.</returns>
        private string GenerateRandomSalt(int length = 128)
        {
            byte[] salt = new byte[length / 8];
            using (System.Security.Cryptography.RandomNumberGenerator rnd =
                System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rnd.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        [MemberNotNull(nameof(Salt), nameof(PasswordHash))]
        public void SetPassword(string password)
        {
            this.Salt = GenerateRandomSalt();
            PasswordHash = CalculateHash(password, Salt);
        }
        public bool CheckPassword(string password) => PasswordHash == CalculateHash(password, Salt);
        

        /// <summary>
        /// Calculates a HMACSHA256 hash value with a given salt.
        /// <returns>Base64 encoded hash.</returns>
        private string CalculateHash(string password, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

            System.Security.Cryptography.HMACSHA256 myHash =
                new System.Security.Cryptography.HMACSHA256(saltBytes);

            byte[] hashedData = myHash.ComputeHash(passwordBytes);

            // Das Bytearray wird als Hexstring zurückgegeben.
            return Convert.ToBase64String(hashedData);
        }
    }

}


