using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmetnickaDela.Data.Models
{
    public class Comments
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
        public string Title { get; set; }
        public int? ProductId { get; set; }
        public UmetnickoDelo? Product { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
