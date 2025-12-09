using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LabWork17.Models;

public partial class Visitor
{
    public int VisitorId { get; set; }

    [Display(Name = "Номер телефона")]
    public string Phone { get; set; } = null!;

    [Display(Name = "Имя")]
    public string? Name { get; set; }

    [Display(Name = "Дата рождения")]
    public DateTime? Birthday { get; set; }

    [Display(Name = "Почта")]
    public string? Email { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
