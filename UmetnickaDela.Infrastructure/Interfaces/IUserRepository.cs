using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmetnickaDela.Data.Models;

namespace UmetnickaDela.Infrastructure.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<bool> acceptJury(int id);
        public Task<bool> declineJury(int id);
    }
}
