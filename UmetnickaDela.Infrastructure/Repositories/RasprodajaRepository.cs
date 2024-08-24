using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class RasprodajaRepository : Repository<Rasprodaja>, IRasprodajaRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public RasprodajaRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<Rasprodaja>> GetRasprodajaWithInclude()
        {
            var rasprodaje = await context.rasprodaja.Include(x => x.UmetnickoDelo).OrderBy(x => x.Id).Take(10).ToListAsync();
            return rasprodaje;
        }
    }
}
