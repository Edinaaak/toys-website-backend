using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmetnickaDela.Contracts.Models.Identity.Request
{
    public class ResetPasswordRequest
    {
        public string token { get; set; }
        public string newPassword { get; set; }
    }
}
