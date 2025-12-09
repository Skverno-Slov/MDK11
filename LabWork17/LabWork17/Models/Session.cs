using System.ComponentModel.DataAnnotations;

namespace LabWork17.Models;

public partial class Session
{
    public int SessionId { get; set; }

    [Display(Name = "Фильм")]
    public int MovieId { get; set; }

    [Display(Name = "Зал")]
    public byte HallId { get; set; }

    [Display(Name = "Цена")]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    [Display(Name = "Дата начала")]
    public DateTime StartDate { get; set; }

    [Display(Name = "3D")]
    public bool Is3d { get; set; }

    [Display(Name = "Зал")]
    public virtual Hall? Hall { get; set; }

    [Display(Name = "Фильм")]
    public virtual Movie? Movie { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
