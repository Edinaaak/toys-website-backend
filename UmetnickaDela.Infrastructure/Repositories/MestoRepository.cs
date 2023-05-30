using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmetnickaDela.Data;
using UmetnickaDela.Data.Models;
using UmetnickaDela.Infrastructure.Interfaces;

namespace UmetnickaDela.Infrastructure.Repositories
{
    public class MestoRepository : Repository<Mesto>, IMestoRepository
    {
        public MestoRepository(DataContext context) : base(context)
        {
        }
    }
}
