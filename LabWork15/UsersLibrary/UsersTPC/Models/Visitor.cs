using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersLibrary.UsersTPC.Models
{
    public class Visitor : User
    {
        public string Phone { get; set; } = null!;

        public decimal CardBalance { get; set; }

        public bool IsBlocked { get; set; }
    }
}
