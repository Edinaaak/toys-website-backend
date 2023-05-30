using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using UmetnickaDela.Contracts.Models;
using UmetnickaDela.Contracts.Models.Masterpiece.Request;
using UmetnickaDela.Contracts.Models.Masterpiece.Response;
using UmetnickaDela.Data.Models;
using UmetnickaDela.Infrastructure;

namespace NBP_projekat.Mediator.Masterpieces
{
    public record GetAllMasterpiecesQuery(MasterpieceFilterRequest Request) : IRequest<Result<IEnumerable<CreateMasterpieceResponse>>>
    {
    }

    public class GetAllMasterpiecesHandler : IRequestHandler<GetAllMasterpiecesQuery, Result<IEnumerable<CreateMasterpieceResponse>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public GetAllMasterpiecesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<IEnumerable<CreateMasterpieceResponse>>> Handle(GetAllMasterpiecesQuery request, CancellationToken cancellationToken)
        {

            var lista = new List<UmetnickoDelo>();
            if(request.Request.salaId.HasValue || request.Request.celinaId.HasValue)
                lista = await unitOfWork.UmetnickoDelo.FilterBySalaTema(request.Request);
            else
            lista = await unitOfWork.UmetnickoDelo.GetAll();
            var mappedList = mapper.Map<IEnumerable<CreateMasterpieceResponse>>(lista);
            return new Result<IEnumerable<CreateMasterpieceResponse>>
            {
                Data = mappedList
            };
        }
    }
}
