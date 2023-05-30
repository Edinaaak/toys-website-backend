using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmetnickaDela.Data.Models
{
    public class UmetnickoDelo
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public float Visina { get; set; }
        public float Sirina { get; set; }
        public string Putanja { get; set; }

        public Sala sala { get; set; }
        public User user { get; set; }
        public TematskaCelina tematskaCelina { get; set; }
        [ForeignKey("sala")]
        [AllowNull]
        public int? salaId { get; set; } = 15;
        [ForeignKey("user")]
        public int slikarId { get; set; }
        [ForeignKey("tematskaCelina")]
        public int? celinaId { get; set; }

        public List<UserDelo> UserDelo { get; set; }
    }
}
