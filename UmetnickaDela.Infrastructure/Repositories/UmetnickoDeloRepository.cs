using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmetnickaDela.Data;
using UmetnickaDela.Data.Models;
using UmetnickaDela.Infrastructure.Interfaces;
using UmetnickaDela.Contracts.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
using UmetnickaDela.Contracts.Models.Masterpiece.Request;

namespace UmetnickaDela.Infrastructure.Repositories
{
    public class UmetnickoDeloRepository : Repository<UmetnickoDelo>, IUmetnickoDelo
    {
        private readonly DataContext dataContext;
        public UmetnickoDeloRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
            this.dataContext = context; 
        }

        public async Task<List<UmetnickoDelo>> FilterBySalaTema(MasterpieceFilterRequest request)
        {
            var lista = await dataContext.umetnickaDela.FilterBySalaTema(request.salaId, request.celinaId).ToListAsync();
            if(lista == null)
                return await dataContext.umetnickaDela.ToListAsync();
            return lista;
        }
    }
}
