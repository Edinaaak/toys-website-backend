﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmetnickaDela.Contracts.Models.Korpa.Request
{
    public class AddKorpaRequest
    {

        public int UserId { get; set; }
        public int DeloId { get; set; }
        public string Condition { get; set; }
        public int Quantity { get; set; }
    }
}
