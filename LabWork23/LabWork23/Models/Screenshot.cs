using System;
using System.Collections.Generic;

namespace LabWork23.Models;

public partial class Screenshot
{
    public int ScreenshotId { get; set; }

    public int GameId { get; set; }

    public string FileName { get; set; } = null!;

    public byte[]? Photo { get; set; }

    public virtual Game Game { get; set; } = null!;
}
