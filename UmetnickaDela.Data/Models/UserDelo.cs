using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmetnickaDela.Data.Models
{
    public class UserDelo
    {
        
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("DeloId")]
        public int DeloId { get; set; }
        public UmetnickoDelo UmetnickoDelo { get; set; }

        public float Ocena { get; set; }
    }
}
