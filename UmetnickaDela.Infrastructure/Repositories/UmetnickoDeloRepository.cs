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
using UmetnickaDela.Contracts.Models.Masterpiece.Response;

namespace UmetnickaDela.Infrastructure.Repositories
{
    public class UmetnickoDeloRepository : Repository<UmetnickoDelo>, IUmetnickoDelo
    {
        private readonly DataContext dataContext;
        private readonly IMapper mapper;
        public UmetnickoDeloRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
            this.dataContext = context; 
            this.mapper = mapper;
        }

      

        public async Task<bool> AddMark(AddMarkRequest request)
        {
            var userDelo = mapper.Map<UserDelo>(request);
            var exist = await dataContext.userDela.Where(x => x.UserId == request.UserId).Where(x => x.DeloId == request.DeloId).FirstOrDefaultAsync();
            if(exist != null)
            {
                return false;
            }
            dataContext.userDela.Add(userDelo);
            var result = await dataContext.SaveChangesAsync();
            return result > 0;

        }

        public async Task<List<UmetnickoDelo>> FilterBySalaTema(MasterpieceFilterRequest request)
        {
          
            var lista = await dataContext.umetnickaDela.FilterBySalaTema(request.salaId, request.celinaId).ToListAsync();
            if(lista == null)
                return await dataContext.umetnickaDela.ToListAsync();
            return lista;
        }

        public async Task<List<UserDelo>> GetMark(int id)
        {
            var mark = await dataContext.userDela.Where(x => x.UserId == id).ToListAsync();
            if(mark == null)
                return null;
            return mark;
        }

        public async Task<CreateMasterpieceResponse> GetWithUserUnitAuditorium(int id)
        {
            var list =  await dataContext.umetnickaDela.Include(x => x.user).Where(x => x.slikarId == x.user.Id).Include(x => x.sala).Where(x => x.salaId == x.sala.Id).Include(x => x.tematskaCelina).Where(x => x.celinaId == x.tematskaCelina.Id).Where(x => x.Id == id).FirstOrDefaultAsync();
            var mappedList = mapper.Map<CreateMasterpieceResponse>(list);
            return mappedList;

        }
    }
}
