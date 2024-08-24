using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmetnickaDela.Infrastructure.Interfaces;

namespace UmetnickaDela.Infrastructure
{
    public interface IUnitOfWork
    {
        ISalaRepository SalaRepository { get; }
        IMestoRepository MestoRepository { get; }
        IUmetnickoDelo UmetnickoDelo { get; }

        IUserRepository userRepository { get; }
        ITematskaCelina TematskaCelina { get; }
        IRasprodajaRepository RasprodajaRepository { get; }
        IKorpeRepository KorpeRepository { get; }
        Task<bool> CompleteAsync();
    }
}
