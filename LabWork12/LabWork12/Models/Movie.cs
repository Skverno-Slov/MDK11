using System.ComponentModel.DataAnnotations.Schema;

namespace LabWork12.Models
{
    [Table("Movie")]
    public partial class Movie
    {
        public int MovieId { get; set; }

        public string Name { get; set; } = null!;

        public short Duration { get; set; }

        public short Year { get; set; }

        public string? Description { get; set; }

        public byte[]? Poster { get; set; }

        public string? AgeRating { get; set; }

        public DateTime? ReleaseBegin { get; set; }

        public DateTime? DistributionEnd { get; set; }

        public bool IsDeleted { get; set; }
    }
}
