using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LabWork17.Models;

public partial class Hall
{
    public byte HallId { get; set; }

    [Display(Name="Кинотеатр")]
    public string Cinema { get; set; } = null!;

    public byte HallNumber { get; set; }

    public byte RowsNumber { get; set; }

    public byte SeatsNumber { get; set; }

    public bool IsVip { get; set; }

    [Display(Name = "Кинотеатр и номер зала")]
    public string? CinemaHallNumber => $"{Cinema}, Номер зала: {HallNumber}";

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
