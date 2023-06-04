using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmetnickaDela.Data.Models;

namespace UmetnickaDela.Contracts.Extensions
{
    public static class MasterpieceExtension
    {
        //query object da sadrzi sve ono za sta zelimo da kreiramo upit
        public static IQueryable<UmetnickoDelo> FilterBySalaTema(this IQueryable<UmetnickoDelo> query, int? salaID, int? temaId)
        {
            if(salaID.HasValue || salaID != null)
                query = query.Where(x => x.salaId == salaID);
            if(temaId.HasValue)
                query = query.Where(x => x.celinaId == temaId);
            return query;
        }
            
        public static IQueryable<UmetnickoDelo> ApplyPaging(this IQueryable<UmetnickoDelo> query, int currPage, int pageSize)
        {
            if (currPage <= 0)
                currPage = 1;
            if(pageSize <= 0 || pageSize > 100)
                pageSize = 20;
            query = query.Skip((currPage - 1) * pageSize)
                .Take(pageSize);
            return query;
        }       

    }
}
