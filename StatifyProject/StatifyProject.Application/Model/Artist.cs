namespace StatifyProject.Application.Model
{

    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Net;
    using System.Numerics;


    [Index(nameof(Name), IsUnique = true)]
    public class Artist
    {
        public Artist(string Name)
        {

            this.Name = Name;
        }

        #pragma warning disable CS8618
        protected Artist() { }
        #pragma warning restore CS8618

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Guid Guid { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        public List<Song> Songs { get; } = new();
        public List<User> FavoriteUsers { get; } = new();
    }

}


