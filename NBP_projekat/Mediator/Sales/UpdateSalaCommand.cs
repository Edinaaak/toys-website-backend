using AutoMapper;
using MediatR;
using UmetnickaDela.Contracts.Models;
using UmetnickaDela.Data.Models;
using UmetnickaDela.Infrastructure;

namespace NBP_projekat.Mediator.Sales
{
    public record UpdateSalaCommand(int id, string name) : IRequest<Result<Sala>>;

    public class UpdateSalaHandler : IRequestHandler<UpdateSalaCommand, Result<Sala>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public UpdateSalaHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<Sala>> Handle(UpdateSalaCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.name))
                return new Result<Sala>
                {
                    Errors = new List<string> { "Name is required"},
                    IsSucces= false
                };
            var sala = await unitOfWork.SalaRepository.getById(request.id);
            if (sala == null)
                return new Result<Sala>
                {
                    Errors = new List<string> { "Sala is not found" },
                    IsSucces = false
                };
            sala.Naziv = request.name;
            var result = await unitOfWork.CompleteAsync();
            if (!result)
                return new Result<Sala>
                {
                    Errors = new List<string> { "Error in saving data" },
                    IsSucces = false
                };
            return new Result<Sala> { Data = sala };
        }
    }

}
