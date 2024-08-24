using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmetnickaDela.Data.Models;

namespace UmetnickaDela.Contracts.Models.Rasprodaja.Requests
{
    public class RasprodajaRequestCreateDTO
    {
        public DateTime Datum { get; set; }
        public decimal Cena { get; set; }
        public int UmetnickoDeloId { get; set; }
    }
}
