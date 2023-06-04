using AutoMapper;
using MediatR;
using System.Reflection.Metadata;
using UmetnickaDela.Contracts.Models;
using UmetnickaDela.Contracts.Models.Masterpiece.Response;
using UmetnickaDela.Infrastructure;

namespace NBP_projekat.Mediator.Masterpieces
{
    public record UpdateSalaCommand (int id, int salaId ) : IRequest<Result<CreateMasterpieceResponse>>
    {
    }

    public class UpdateSalaHandler : IRequestHandler<UpdateSalaCommand, Result<CreateMasterpieceResponse>>
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMapper mapper;
        public UpdateSalaHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.UnitOfWork = unitOfWork;
        }
        
        public async Task<Result<CreateMasterpieceResponse>> Handle(UpdateSalaCommand request, CancellationToken cancellationToken)
        {
            var result = await UnitOfWork.UmetnickoDelo.UpdateAuditorium(request.id, request.salaId);
            if (result == null)
                return new Result<CreateMasterpieceResponse>
                {
                    Errors = new List<string> { "This masterpiece does not exist" },
                    IsSucces = false
                };
            var mappedMasterpiecce = mapper.Map<CreateMasterpieceResponse>(result);
            return new Result<CreateMasterpieceResponse>

            {
                Data = mappedMasterpiecce
            };

        }
    }
}
