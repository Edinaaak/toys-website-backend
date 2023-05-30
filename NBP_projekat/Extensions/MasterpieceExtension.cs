using UmetnickaDela.Data.Models;

namespace NBP_projekat.Extensions
{
    public static class MasterpieceExtension
    {
        public static IQueryable<UmetnickoDelo> FilterBySala(this IQueryable<UmetnickoDelo> query, int? salaID) =>
            salaID.HasValue ? query : query.Where(x => x.salaId == salaID);

        public static IQueryable<UmetnickoDelo> FilterByTema (this IQueryable<UmetnickoDelo> query, int? celinaId) =>
            celinaId.HasValue? query : query.Where( x=> x.celinaId == celinaId); 

}
}
