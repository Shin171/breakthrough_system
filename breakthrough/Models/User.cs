using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace breakthrough.Models
{
    public class User
    {
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } 
    }

}