using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmetnickaDela.Contracts.Models.Korpa.Request;
using UmetnickaDela.Data;
using UmetnickaDela.Data.Models;
using UmetnickaDela.Infrastructure.Interfaces;

namespace UmetnickaDela.Infrastructure.Repositories
{
    public class KorpeRepository : Repository<Korpa>, IKorpeRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public KorpeRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<bool> AddProizvodToKorpa(AddKorpaRequest addKorpaRequest)
        {
            var korpaToAdd = mapper.Map<Korpa>(addKorpaRequest);
            context.korpe.Add(korpaToAdd);
            var res = await context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Korpa>> GetKorpaWithIncludeByUserId(int userId)
        {
            
            var products = await context.korpe.Where(x => x.UserId == userId).Include(x => x.UmetnickoDelo).ToListAsync();
            return products;
        }
    }
}
