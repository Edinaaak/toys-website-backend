﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmetnickaDela.Contracts.Models.Masterpiece.Request
{
    public  class AddMarkRequest
    {
        public float Ocena { get; set; }
        public int UserId { get; set; }
        public int DeloId { get; set; }
    }
}
