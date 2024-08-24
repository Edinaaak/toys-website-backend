using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmetnickaDela.Contracts.Models.Masterpiece.Request
{
    public class MasterpieceFilterRequest
    {
        public int? celinaId { get; set; }
        public int? salaId { get; set; }
        public decimal? cenaOd { get; set; }
        public int currPage { get; set; }
        public int pageSize { get; set; }
        
      
    }
}
