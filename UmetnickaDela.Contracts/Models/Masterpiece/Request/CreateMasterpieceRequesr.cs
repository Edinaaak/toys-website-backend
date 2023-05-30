using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmetnickaDela.Data.Models;

namespace UmetnickaDela.Contracts.Models.Masterpiece.Request
{
    public class CreateMasterpieceRequesr
    {
       
        public string Naziv { get; set; }
        public float Visina { get; set; }
        public float Sirina { get; set; }
        public IFormFile Putanja { get; set; }
      
        public int slikarId { get; set; }
        public int celinaId { get; set; }

        public int salaId { get; set; } = 15;

    }
}
