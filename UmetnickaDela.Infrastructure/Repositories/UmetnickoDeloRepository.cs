﻿using AutoMapper;
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
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

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

       

        public async Task<float> AverageRating(int idMasterpiece)
        {
            float ocena = await dataContext.userDela
                .Where(x => x.DeloId == idMasterpiece)
                .AverageAsync(x => x.Ocena);
            if(ocena > 0) 
                return ocena;
            return 0;
        }

        public async Task<List<UmetnickoDelo>> FilterBySalaTema(MasterpieceFilterRequest request)
        {
          
            var lista = await dataContext.umetnickaDela.FilterBySalaTema(request.salaId, request.celinaId, request.cenaOd).ToListAsync();
            if(lista == null)
                return await dataContext.umetnickaDela.ToListAsync();
            return lista;
        }

        public async Task<List<UmetnickoDelo>> ApplyPaging(int currPage, int pageSize)
        {
            var query = (await dataContext.umetnickaDela.ToListAsync()).AsQueryable();
            query = query.ApplyPaging(currPage, pageSize);
            if (query == null)
                return null;
            var listToShow = mapper.Map<List<UmetnickoDelo>>(query.ToList());
            return listToShow;

        }

        public async Task<List<UserDelo>> GetMark(int id)
        {
            var mark = await dataContext.userDela.Where(x => x.UserId == id).ToListAsync();
            if(mark == null)
                return null;
            return mark;
        }

        public async Task<List<UmetnickoDelo>> getMasterpieceByUser(int idUser)
        {
            var list = await dataContext.umetnickaDela.Where(x => x.slikarId == idUser).ToListAsync();
            if (list == null)
                return null;
            return list;
        }

        public async Task<CreateMasterpieceResponse> GetWithUserUnitAuditorium(int id)
        {
            var list =  await dataContext.umetnickaDela.Include(x => x.user).Where(x => x.slikarId == x.user.Id).Include(x => x.sala).Where(x => x.salaId == x.sala.Id).Include(x => x.tematskaCelina).Where(x => x.celinaId == x.tematskaCelina.Id).Where(x => x.Id == id).FirstOrDefaultAsync();
            var mappedList = mapper.Map<CreateMasterpieceResponse>(list);
            return mappedList;

        }

        public async Task<UmetnickoDelo> UpdateAuditorium(int id, int salaId)
        {
            var masterpiece = await dataContext.umetnickaDela.FindAsync(id);
            if (masterpiece == null)
                return null;
            masterpiece.salaId = salaId;
            await dataContext.SaveChangesAsync();
            return masterpiece;
        }

        public async Task<GetReviewsResponse> GetReviews(int deloId)
        {
            var results = await dataContext.userDela.Where(x => x.DeloId == deloId).ToListAsync();
            GetReviewsResponse getReviews = new GetReviewsResponse();
            foreach (var item in results)
            {
                if(item.Ocena == 1)
                {
                    getReviews.ReviewsForOne++;
                }
                else if(item.Ocena == 2)
                {
                    getReviews.ReviewsForTwo++;
                }
                else if(item.Ocena == 3)
                {
                    getReviews.ReviewsForThree++;
                }
                else if(item.Ocena == 4)
                {
                    getReviews.ReviewsForFour++;
                }
                else if(item.Ocena == 5)
                {
                    getReviews.ReviewsForFive++;
                }   
            }
            getReviews.TotalReviews = results.Count;
            return getReviews;
        }
    }
}
