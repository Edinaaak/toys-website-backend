using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmetnickaDela.Contracts.Models
{
    public class Result<T>
    {
        public bool IsSucces { get; set; } = true;
        public List<string> Errors { get; set; }
        public T Data { get; set; }
       

        public Result()
        {
            Errors = new List<string>();
        }
    }
}
