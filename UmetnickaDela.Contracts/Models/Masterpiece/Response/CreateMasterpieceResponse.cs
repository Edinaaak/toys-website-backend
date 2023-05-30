using AutoMapper.Configuration.Conventions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmetnickaDela.Data.Models;

namespace UmetnickaDela.Contracts.Models.Masterpiece.Response
{
    public class CreateMasterpieceResponse
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public float Visina { get; set; }
        public float Sirina { get; set; }
        public string Putanja { get; set; }

        public UmetnickaDela.Data.Models.Sala sala { get; set; }
        public User user { get; set; }
        public TematskaCelina tematskaCelina { get; set; }
    }
}
