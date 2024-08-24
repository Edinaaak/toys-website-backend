using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmetnickaDela.Data.Models
{
    public class Rasprodaja
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public decimal Cena { get; set; }
        public UmetnickoDelo UmetnickoDelo { get; set; }
        [ForeignKey("UmetnickoDelo")]
        public int UmetnickoDeloId { get; set; }


    }
}
