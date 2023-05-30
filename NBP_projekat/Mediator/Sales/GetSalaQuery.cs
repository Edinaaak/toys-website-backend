using AutoMapper;
using MediatR;
using System.Transactions;
using UmetnickaDela.Contracts.Models;
using UmetnickaDela.Data.Models;
using UmetnickaDela.Infrastructure;

namespace NBP_projekat.Mediator.Sales
{
    public record GetSalaQuery(int id) : IRequest<Result<Sala>>;

    public class GetOneSalaHandler : IRequestHandler<GetSalaQuery, Result<Sala>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public GetOneSalaHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<Sala>> Handle(GetSalaQuery request, CancellationToken cancellationToken)
        {
            var sala = await unitOfWork.SalaRepository.getById(request.id);
            if(sala == null)
            {
                return new Result<Sala>
                {
                    Errors = new List<string> { $"Sala with {request.id} not found in database" },
                    IsSucces = false
                };
            }
            return new Result<Sala> { Data = sala };
        }
    }

}
