using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmetnickaDela.Contracts.Models.Masterpiece.Request;
using UmetnickaDela.Data.Models;

namespace UmetnickaDela.Infrastructure.Interfaces
{
    public interface IUmetnickoDelo : IRepository<UmetnickoDelo>
    {
        Task<List<UmetnickoDelo>> FilterBySalaTema(MasterpieceFilterRequest request);
        Task<bool> AddMark(AddMarkRequest request);
    }
}
