using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersLibrary.UsersTPC.Models
{
    public class TicketSeller : User
    {
        public string FullName { get; set; } = null!;

        public decimal? Salary { get; set; }
    }
}
