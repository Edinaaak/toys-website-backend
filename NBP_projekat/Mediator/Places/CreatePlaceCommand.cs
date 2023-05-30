using AutoMapper;
using MediatR;
using UmetnickaDela.Contracts.Models;
using UmetnickaDela.Contracts.Models.Mesto.Request;
using UmetnickaDela.Data.Models;
using UmetnickaDela.Infrastructure;

namespace NBP_projekat.Mediator.Places
{
    public record CreatePlaceCommand(CreatePlaceRequest Request) : IRequest<Result<Mesto>>;

    public class CreatePlaceHandler : IRequestHandler<CreatePlaceCommand, Result<Mesto>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public CreatePlaceHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<Mesto>> Handle(CreatePlaceCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Request.Naziv))
                return new Result<Mesto>
                {
                    Errors = new List<string> { "Name is required" },
                    IsSucces = false

                };
            var mesto = new Mesto
            { 
                Naziv = request.Request.Naziv,
                Lokacija = request.Request.Lokacija
            };
            await unitOfWork.MestoRepository.Add(mesto);
            var result = await unitOfWork.CompleteAsync();
            if (!result)
                return new Result<Mesto> 
                {
                    Errors = new List<string> { "Error in adding data" },
                    IsSucces = false 
                };
            return new Result<Mesto> { Data = mesto };

        }
    }

}
