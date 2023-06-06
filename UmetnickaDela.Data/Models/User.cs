using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmetnickaDela.Data.Models
{
    public class User : IdentityUser<int>
    {
       
       
        [Required]
        [MaxLength(10)]
        public string Ime { get; set; }
        [Required]
        [MaxLength(15)]
        public string Prezime { get; set; }
        [RegularExpression("^\\d{13}$")]
        public string JMBG { get; set; }
        public string NazivMesta { get; set; }
        public string Ptt { get; set; }
        public List<UserDelo> userDelo { get; set; }
        


    }
}
