using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaDbLibrary.Models
{
    [Table("Movie")]
    public class Movie
    {
        public int MovieId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public byte[]? Poster { get; set; }
        public string? AgeRating { get; set; }
        public DateTime? ReleaseBegin { get; set; } 
        public DateTime? DistributionEnd { get; set; }
        public IEnumerable<Genre> Genres { get; set; } = null!;
    }
}
