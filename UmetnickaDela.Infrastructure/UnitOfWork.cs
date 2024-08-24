﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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

        public UnitOfWork(DataContext context, ISalaRepository salaRepository, IMestoRepository mestoRepository, IUmetnickoDelo umetnickoDelo, IUserRepository userRepository, ITematskaCelina tematskaCelina, IRasprodajaRepository rasprodajaRepository, IKorpeRepository korpeRepository)
        {
            this.context = context;
            SalaRepository = salaRepository;
            MestoRepository = mestoRepository;
            UmetnickoDelo = umetnickoDelo;
            this.userRepository = userRepository;
            TematskaCelina = tematskaCelina;
            RasprodajaRepository = rasprodajaRepository;
            KorpeRepository = korpeRepository;
        }

        public ISalaRepository SalaRepository { get;}
        public IMestoRepository MestoRepository { get;}

        public IUmetnickoDelo UmetnickoDelo { get;}
        public IUserRepository userRepository { get;}

        public ITematskaCelina TematskaCelina { get;}

        public IRasprodajaRepository RasprodajaRepository { get;}

        public IKorpeRepository KorpeRepository { get;}

        public async Task<bool> CompleteAsync()
        {
            var result = await context.SaveChangesAsync();
            return result > 0;
        }
    }
}
