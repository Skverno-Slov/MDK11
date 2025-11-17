using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersLibrary.UsersTPH.Models
{
    public class TicketSeller : User
    {
        public string FullName { get; set; } = null!;

        public decimal? Salary { get; set; }
    }
}
