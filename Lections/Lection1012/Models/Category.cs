using System;
using System.Collections.Generic;

namespace Lection1012.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
    public string? Description { get; set; }
}
