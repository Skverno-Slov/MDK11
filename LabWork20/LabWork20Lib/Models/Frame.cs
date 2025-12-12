using System;
using System.Collections.Generic;

namespace LabWork20Lib.Models;

public partial class Frame
{
    public int FrameId { get; set; }

    public int MovieId { get; set; }

    public string? FileName { get; set; }

    public virtual Movie Movie { get; set; } = null!;
}
