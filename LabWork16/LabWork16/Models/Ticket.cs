using System;
using System.Collections.Generic;

namespace LabWork16.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    public int VisitorId { get; set; }

    public int SessionId { get; set; }

    public byte Row { get; set; }

    public byte Seat { get; set; }
}
