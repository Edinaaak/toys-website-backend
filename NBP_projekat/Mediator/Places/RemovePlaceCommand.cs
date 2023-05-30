using MediatR;
using UmetnickaDela.Contracts.Models;
using UmetnickaDela.Infrastructure;

namespace NBP_projekat.Mediator.Places
{
    public record RemovePlaceCommand(int id) : IRequest<Result<bool>> { }

    public class RemovePlaceHandler : IRequestHandler<RemovePlaceCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        public RemovePlaceHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(RemovePlaceCommand request, CancellationToken cancellationToken)
        {
            var place = await unitOfWork.MestoRepository.getById(request.id);
            if (place == null)
            {
                return new Result<bool> {
                    Errors = new List<string> { "Place is not found" },
                    IsSucces = false
                };
            }
            var result = await unitOfWork.MestoRepository.Delete(place);
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
