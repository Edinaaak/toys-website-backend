using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmetnickaDela.Contracts.Models.Identity.Request;
using UmetnickaDela.Contracts.Models.Identity.Response;
using UmetnickaDela.Data.Models;

namespace UmetnickaDela.Infrastructure.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<bool> acceptJury(int id);
        public Task<bool> declineJury(int id);
        public Task<UserResponse> register(RegisterRequest request);
        public Task<LoginResponse> login (LoginRequest request);
        public Task<LoginResponse> googleLogin(string email);
        public Task<string> forgotPassword(string email);
        public Task<string> checkToken(string token);
        public Task<bool> resetPassword(ResetPasswordRequest request);
    }
}
