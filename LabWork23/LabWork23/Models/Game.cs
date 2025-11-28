using System;
using System.Collections.Generic;

namespace LabWork23.Models;

public partial class Game
{
    public int GameId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int IdCategory { get; set; }

    public decimal Price { get; set; }

    public string? LogoFile { get; set; }
}
