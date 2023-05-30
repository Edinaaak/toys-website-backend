using AutoMapper;
using MediatR;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UmetnickaDela.Contracts.Models;
using UmetnickaDela.Data.Models;
using UmetnickaDela.Infrastructure;

namespace NBP_projekat.Mediator.Sales
{
    public class GetAllSalaQuery : IRequest<Result<IEnumerable<Sala>>> { }

    public class GetSalaHandler : IRequestHandler<GetAllSalaQuery, Result<IEnumerable<Sala>>>

    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public GetSalaHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<IEnumerable<Sala>>> Handle(GetAllSalaQuery request, CancellationToken cancellationToken)
        {
            var lista = await unitOfWork.SalaRepository.GetAll();
            var result = new Result<IEnumerable<Sala>>() { Data = lista };
            return result;
        }
    }


}
