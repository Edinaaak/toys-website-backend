using MediatR;
using UmetnickaDela.Contracts.Models;
using UmetnickaDela.Infrastructure;

namespace NBP_projekat.Mediator.Masterpieces
{
    public record DeleteMasterpieceCommand (int id) : IRequest<Result<bool>>
    {
    }

    public class DeleteMasterpieceHandler : IRequestHandler<DeleteMasterpieceCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        public DeleteMasterpieceHandler(IUnitOfWork unitOfWork)
        {
                this.unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(DeleteMasterpieceCommand request, CancellationToken cancellationToken)
        {
            var piece = await unitOfWork.UmetnickoDelo.getById(request.id);
            if(piece == null) 
                return new Result<bool> { 
                Errors = new List<string> { $"Can not find masterpiece with {request.id}" },
                IsSucces = false
                };
            var result = await unitOfWork.UmetnickoDelo.Delete(piece);
            if (!result)
                return new Result<bool>
                {
                    IsSucces = false
                };
            return new Result<bool> 
            {
                IsSucces = true
            };
        }
    }

}
