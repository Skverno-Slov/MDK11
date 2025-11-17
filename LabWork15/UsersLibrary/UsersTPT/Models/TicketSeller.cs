using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersLibrary.UsersTPT.Models
{
    [Table("TicketSeller")]
    public class TicketSeller : User
    {
        public string FullName { get; set; } = null!;

        public decimal? Salary { get; set; }
    }
}
