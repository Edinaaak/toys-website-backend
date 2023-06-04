using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmetnickaDela.Data;
using UmetnickaDela.Infrastructure.Interfaces;
using UmetnickaDela.Infrastructure.Repositories;

namespace UmetnickaDela.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext context;

        public UnitOfWork(DataContext context, ISalaRepository salaRepository, IMestoRepository mestoRepository, IUmetnickoDelo umetnickoDelo, IUserRepository userRepository)
        {
            this.context = context;
            SalaRepository = salaRepository;
            MestoRepository = mestoRepository;
            UmetnickoDelo = umetnickoDelo;
            this.userRepository = userRepository;
        }

        public ISalaRepository SalaRepository { get;}
        public IMestoRepository MestoRepository { get;}

        public IUmetnickoDelo UmetnickoDelo { get;}
        public IUserRepository userRepository { get;}

        public async Task<bool> CompleteAsync()
        {
            var result = await context.SaveChangesAsync();
            return result > 0;
        }
    }
}
