using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmetnickaDela.Contracts.Models.Identity.Request
{
    public class RegisterRequest
    {
        public string Ime { get; set; }

        public string Prezime { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }
        public string JMBG { get; set; }

        public string Password { get; set; }

        public string NazivMesta { get; set; }
        public string Ptt { get; set; }

        public int  Role { get; set; }
    }
}
