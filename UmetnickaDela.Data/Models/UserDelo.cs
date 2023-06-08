using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace UmetnickaDela.Data.Models
{
    public class UserDelo
    {
        [Key]
        public int Id { get; set; }
       
        [ForeignKey(nameof(User))]
        [AllowNull]
        public int? UserId { get; set; }
        [AllowNull]
        public User? User { get; set; }
        [ForeignKey(nameof(UmetnickoDelo))]
        [AllowNull]
        public int? DeloId { get; set; }
        [AllowNull]
        public UmetnickoDelo? UmetnickoDelo { get; set; }

        public float Ocena { get; set; }
    }
}
