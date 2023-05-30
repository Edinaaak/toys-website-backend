using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using UmetnickaDela.Contracts.Models;
using UmetnickaDela.Contracts.Models.Sala.Request;
using UmetnickaDela.Data;
using UmetnickaDela.Data.Models;
using UmetnickaDela.Infrastructure;

namespace NBP_projekat.Mediator.Sales
{
    public record CreateSalaCommand (CreateSalaRequest salaRequest) : IRequest<Result<Sala>>;

    public class CreateSalaHandler : IRequestHandler<CreateSalaCommand, Result<Sala>>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public CreateSalaHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Sala>> Handle(CreateSalaCommand request, CancellationToken cancellationToken)
        {

            if (string.IsNullOrEmpty(request.salaRequest.Naziv) || request.salaRequest.Povrsina == null)
                return new Result<Sala>
                {
                    Errors = new List<string>{ "Name or area is required" },
                    IsSucces = false
                };
            var newSala = new Sala
            {
                Naziv = request.salaRequest.Naziv,
                Povrsina = request.salaRequest.Povrsina,
                MestoId = request.salaRequest.MestoId

            };
            await unitOfWork.SalaRepository.Add(newSala);
            var result = await unitOfWork.CompleteAsync();
            if (!result)
                return new Result<Sala>
                {
                    Errors = new List<string> { "Something is not correct with request data" },
                    IsSucces = false
                };

            return new Result<Sala> { Data = newSala };
            





        }
    }
}
