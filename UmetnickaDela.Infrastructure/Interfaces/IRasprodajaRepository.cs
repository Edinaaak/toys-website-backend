﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmetnickaDela.Data.Models;

namespace UmetnickaDela.Infrastructure.Interfaces
{
    public interface IRasprodajaRepository: IRepository<Rasprodaja>
    {

        Task<List<Rasprodaja>> GetRasprodajaWithInclude();
    }
}
