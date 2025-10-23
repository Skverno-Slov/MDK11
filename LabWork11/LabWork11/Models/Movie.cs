using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabWork11.Models;

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

    public DateOnly? ReleaseBegin { get; set; }

    public DateOnly? DistributionEnd { get; set; }

    public bool IsDeleted { get; set; }
}
