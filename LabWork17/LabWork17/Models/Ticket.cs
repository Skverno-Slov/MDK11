using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LabWork17.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    public int VisitorId { get; set; }

    public int SessionId { get; set; }

    public byte Row { get; set; }

    public byte Seat { get; set; }

    public virtual Session? Session { get; set; }

    public virtual Visitor? Visitor { get; set; }
}
