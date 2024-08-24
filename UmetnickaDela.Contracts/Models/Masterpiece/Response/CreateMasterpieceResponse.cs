using AutoMapper.Configuration.Conventions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmetnickaDela.Contracts.Models.Identity.Response;
using UmetnickaDela.Data.Models;

namespace UmetnickaDela.Contracts.Models.Masterpiece.Response
{
    public class CreateMasterpieceResponse
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public decimal Cena { get; set; }
        public string Opis { get; set; }
        public string Putanja { get; set; }
        public float Ocena { get; set; }

        public UmetnickaDela.Data.Models.Sala sala { get; set; }
        public UserResponse user { get; set; }
        public TematskaCelina tematskaCelina { get; set; }
    }
}
