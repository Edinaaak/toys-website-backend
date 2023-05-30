using MediatR;
using UmetnickaDela.Contracts.Models;
using UmetnickaDela.Infrastructure;

namespace NBP_projekat.Mediator.Sales
{
    public record RemoveSalaCommand (int id) : IRequest<Result<bool>>;

    public class RemoveSalaHandler : IRequestHandler<RemoveSalaCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        public RemoveSalaHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(RemoveSalaCommand request, CancellationToken cancellationToken)
        {
            var sala = await unitOfWork.SalaRepository.getById(request.id);
            if(sala == null)
            {
                return new Result<bool>
                {
                    Errors = new List<string> { "Sala is not found" },
                    IsSucces = false
                };
            }
            var result = await unitOfWork.SalaRepository.Delete(sala);
            if (!result)
                return new Result<bool>
                {
                    IsSucces = false
                };
            return new Result<bool>
            { IsSucces = true};


        }
    }
}
