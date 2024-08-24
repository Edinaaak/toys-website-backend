using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmetnickaDela.Contracts.Models.Masterpiece.Request;
using UmetnickaDela.Contracts.Models.Masterpiece.Response;
using UmetnickaDela.Data.Models;

namespace UmetnickaDela.Infrastructure.Interfaces
{
    public interface IUmetnickoDelo : IRepository<UmetnickoDelo>
    {
        Task<List<UmetnickoDelo>> FilterBySalaTema(MasterpieceFilterRequest request);
        Task<List<UmetnickoDelo>> ApplyPaging(int currPage, int pageSize);
        Task<bool> AddMark(AddMarkRequest request);

        Task<List<UserDelo>> GetMark (int idUser);
        Task<CreateMasterpieceResponse> GetWithUserUnitAuditorium(int idUser);
        Task<List<UmetnickoDelo>> getMasterpieceByUser (int idUser);
        Task<float> AverageRating (int idMasterpiece);
        Task<UmetnickoDelo> UpdateAuditorium(int id, int salaId);

        Task<GetReviewsResponse> GetReviews (int deloId);

    }
}
