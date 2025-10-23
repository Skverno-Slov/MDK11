﻿using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaDbLibrary.Models
{
    [Table("Genre")]
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; } = null!;
        public IEnumerable<Movie> Movies { get; set; } = null!;
    }
}
