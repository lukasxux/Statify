namespace StatifyProject.Application.Model
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    [Index(nameof(Id), IsUnique = true)]
    [Index(nameof(Title), IsUnique = true)]

    public class Song
    {
        public Song(string Title, Artist Artist, DateTime Created_at, TimeSpan Length, 
            string? ImageUrl = null,
            string? Link = null)
        {
            this.Title = Title;
            this.Artist = Artist;
            this.Created_at = Created_at;
            this.Length = Length;
            this.ImageUrl = ImageUrl;
            this.Link = Link;
        }


#pragma warning disable CS8618
        protected Song() { }
#pragma warning restore CS8618


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Guid Guid { get; set; }

        [MaxLength(40)]
        public string Title { get; set; }
        public Artist Artist { get; set; }
        public DateTime Created_at { get; set; }
        public TimeSpan Length { get; set; }
        public string? ImageUrl { get; set; }
        public string? Link { get; set; }
        public List<User> FavoriteUsers { get; } = new();
    }
}


