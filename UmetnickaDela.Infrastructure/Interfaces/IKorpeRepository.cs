using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmetnickaDela.Contracts.Models.Korpa.Request;
using UmetnickaDela.Data.Models;

namespace UmetnickaDela.Infrastructure.Interfaces
{
    public interface IKorpeRepository : IRepository<Korpa>
    {

        Task<List<Korpa>> GetKorpaWithIncludeByUserId(int userId);
        Task<bool> AddProizvodToKorpa(AddKorpaRequest addKorpaRequest);
    }
}
