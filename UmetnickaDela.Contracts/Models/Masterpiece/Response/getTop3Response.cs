using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmetnickaDela.Contracts.Models.Masterpiece.Response
{
    public class getTop3Response
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Putanja { get; set; }
    }
}
