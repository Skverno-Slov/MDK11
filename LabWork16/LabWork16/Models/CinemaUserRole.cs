using System;
using System.Collections.Generic;

namespace LabWork16.Models;

public partial class CinemaUserRole
{
    public int RoleId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<CinemaUser> CinemaUsers { get; set; } = new List<CinemaUser>();
}
