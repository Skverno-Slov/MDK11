﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace CinemaDbLibrary.Models
{
    [Table("Visitor")]
    public class Visitor
    {
        public int VisitorId { get; set; }
        public string Phone { get; set; } = null!;
        public string? Name { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; } = null!;

        public IEnumerable<Ticket>? Tickets { get; set; }
    }
}
