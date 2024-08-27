using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmetnickaDela.Data.Models;

namespace UmetnickaDela.Contracts.Models.Comments.Request
{
    public class AddCommentRequest
    {
       
        public string Text { get; set; }
        public string Date { get; set; }
        public string Title { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }

    }
}
