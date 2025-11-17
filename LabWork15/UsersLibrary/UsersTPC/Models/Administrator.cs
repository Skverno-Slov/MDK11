using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersLibrary.UsersTPC.Models
{
    public class Administrator : User
    {
        public string Phone { get; set; } = null!;

        public string Email { get; set; } = null!;

        public decimal? Salary { get; set; }
    }
}
