using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmetnickaDela.Data.Models
{
    public class Sala
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public double Povrsina { get; set; }

        public Mesto? mesto { get; set; }

        [ForeignKey("mesto")]
        public int? MestoId { get; set; }

        
    }
}
