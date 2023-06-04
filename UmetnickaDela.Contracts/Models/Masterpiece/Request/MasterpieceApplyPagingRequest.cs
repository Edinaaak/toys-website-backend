using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmetnickaDela.Contracts.Models.Masterpiece.Request
{
    public class MasterpieceApplyPagingRequest
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
    }
}
