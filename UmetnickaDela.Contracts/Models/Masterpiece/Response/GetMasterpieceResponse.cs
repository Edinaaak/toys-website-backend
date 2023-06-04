using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmetnickaDela.Contracts.Models.Masterpiece.Response
{
    public class GetMasterpieceResponse
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public float Visina { get; set; }
        public float Sirina { get; set; }
        public string Putanja { get; set; }
        public float Ocena { get; set; }

    }
}