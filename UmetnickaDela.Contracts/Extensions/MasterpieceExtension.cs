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
        public static IQueryable<UmetnickoDelo> FilterBySalaTema(this IQueryable<UmetnickoDelo> query, int? salaID, int? temaId)
        {
            if(salaID.HasValue || salaID != null)
                query = query.Where(x => x.salaId == salaID);
            if(temaId.HasValue)
                query = query.Where(x => x.celinaId == temaId);
            return query;
        }
            

       
    }
}
