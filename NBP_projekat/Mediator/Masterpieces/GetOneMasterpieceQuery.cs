using AutoMapper;
using MediatR;
using UmetnickaDela.Contracts.Models;
using UmetnickaDela.Contracts.Models.Masterpiece.Response;
using UmetnickaDela.Infrastructure;

namespace NBP_projekat.Mediator.Masterpieces
{
    public record GetOneMasterpieceQuery(int id) : IRequest<Result<CreateMasterpieceResponse>>
    {
    }

    public class GetOneMasterpieceHandler : IRequestHandler<GetOneMasterpieceQuery, Result<CreateMasterpieceResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetOneMasterpieceHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<CreateMasterpieceResponse>> Handle(GetOneMasterpieceQuery request, CancellationToken cancellationToken)
        {
            var piece = await unitOfWork.UmetnickoDelo.GetWithUserUnitAuditorium(request.id);
            if (piece == null)
            {
                return new Result<CreateMasterpieceResponse>
                {
                    Errors = new List<string> { $"Can not find masterpiece with id: {request.id}" },
                    IsSucces = false
                };

            }

            return new Result<CreateMasterpieceResponse>
            {
                Data = piece
            };
        }
    }
}
