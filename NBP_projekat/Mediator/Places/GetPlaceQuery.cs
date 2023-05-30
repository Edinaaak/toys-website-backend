using MediatR;
using UmetnickaDela.Contracts.Models;
using UmetnickaDela.Data.Models;
using UmetnickaDela.Infrastructure;

namespace NBP_projekat.Mediator.Places
{
    public record GetPlaceQuery(int id) : IRequest<Result<Mesto>>
    {
    }

    public class GetPlaceHandler : IRequestHandler<GetPlaceQuery, Result<Mesto>>
    {
        private readonly IUnitOfWork unitOfWork;
        public GetPlaceHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Mesto>> Handle(GetPlaceQuery request, CancellationToken cancellationToken)
        {
            var mesto = await unitOfWork.MestoRepository.getById(request.id);
            if(mesto == null)
            {
                return new Result<Mesto>
                {
                    Errors = new List<string> { $"Place with {request.id} is not found" },
                    IsSucces = false
                };
            }
            return new Result<Mesto>
            {
                Data = mesto
            };
        }
    }

}
