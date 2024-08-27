using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmetnickaDela.Contracts.Models.Korpa.Request
{
    public class UpdateKorpaRequest
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }
}
