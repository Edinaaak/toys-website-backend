using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmetnickaDela.Contracts.Models.Masterpiece.Response;
using UmetnickaDela.Data.Models;

namespace UmetnickaDela.Contracts.Models.Identity.Response
{
    public class LoginResponse
    {
        public DateTime expires { get; set; }
        public string token { get; set; }
        public UserResponse painter { get; set; }
        public List<GetMasterpieceResponse> dela { get; set; }
        public IList<string> role { get; set; }

        public string error { get; set; } = "";
    }
}
