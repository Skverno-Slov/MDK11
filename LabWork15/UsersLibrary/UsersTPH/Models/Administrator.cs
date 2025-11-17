using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersLibrary.UsersTPH.Models
{
    public class Administrator : User
    {
        public string Phone { get; set; } = null!;

        public string Email { get; set; } = null!;

        public decimal? Salary { get; set; }
    }
}
