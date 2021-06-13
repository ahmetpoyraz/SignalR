using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRProjectUI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string userName { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string password { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string  DateOfBirth { get; set; }
        public int EmailConfirm { get; set; }

    }
}
