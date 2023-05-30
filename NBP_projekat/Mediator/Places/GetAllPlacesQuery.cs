using MediatR;
using UmetnickaDela.Contracts.Models;
using UmetnickaDela.Data.Models;
using UmetnickaDela.Infrastructure;

namespace NBP_projekat.Mediator.Places
{
    public class GetAllPlacesQuery  : IRequest<Result<IEnumerable<Mesto>>> { }

    public class GetAllHandler : IRequestHandler<GetAllPlacesQuery, Result<IEnumerable<Mesto>>>
    {
        private readonly IUnitOfWork unitOfWork;
        public GetAllHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
                
        }
        public async Task<Result<IEnumerable<Mesto>>> Handle(GetAllPlacesQuery request, CancellationToken cancellationToken)
        {
            var lista = await unitOfWork.MestoRepository.GetAll();
            return new Result<IEnumerable<Mesto>> { Data = lista };
        }
    }
}
