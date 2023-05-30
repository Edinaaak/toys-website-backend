using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmetnickaDela.Contracts.Models.Sala.Request
{
    public class CreateSalaRequest
    {
        public string Naziv { get; set; }
        public double Povrsina { get; set; }
        public int MestoId { get; set; }
    }
}
