using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmetnickaDela.Contracts.Models.Masterpiece.Request
{
    public class UpdateMasterpieceRequest
    {
        public string Naziv { get; set; }
        public float Visina { get; set; }
        public float Sirina { get; set; }
        public IFormFile Putanja { get; set; }
        public int salaId { get; set; }
    }
}
