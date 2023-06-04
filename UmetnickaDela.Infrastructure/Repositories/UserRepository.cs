using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmetnickaDela.Data;
using UmetnickaDela.Data.Models;
using UmetnickaDela.Infrastructure.Interfaces;

namespace UmetnickaDela.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly UserManager<User> userManager;

        public UserRepository(DataContext context, UserManager<User> manager) : base(context)
        {
            this.userManager = manager;
        }

        public async Task<bool> acceptJury(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null) 
                return false;
            user.EmailConfirmed = true;
            await userManager.UpdateAsync(user);
            return true;
        }

        public async Task<bool> declineJury(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return false;
            await userManager.DeleteAsync(user);
            return true;

        }
    }
}
