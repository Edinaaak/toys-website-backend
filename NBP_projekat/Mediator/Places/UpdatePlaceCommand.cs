using AutoMapper.Configuration.Conventions;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using UmetnickaDela.Contracts.Models;
using UmetnickaDela.Contracts.Models.Mesto.Request;
using UmetnickaDela.Data.Models;
using UmetnickaDela.Infrastructure;

namespace NBP_projekat.Mediator.Places
{
    public record UpdatePlaceCommand(int id, CreatePlaceRequest Request) : IRequest<Result<Mesto>> { }

    public class UpdatePlaceHandler : IRequestHandler<UpdatePlaceCommand, Result<Mesto>>
    {
        private readonly IUnitOfWork unitOfWork;
        public UpdatePlaceHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Mesto>> Handle(UpdatePlaceCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Request.Naziv))
                return new Result<Mesto>()
                {
                    Errors  = new List<string> { "Name is required"},
                    IsSucces = false
                };
            var place = await unitOfWork.MestoRepository.getById(request.id);
            if (place == null)
                return new Result<Mesto>
                {
                    Errors = new List<string> { $"Place is not found" },
                    IsSucces = false
                };
            place.Naziv = request.Request.Naziv;
            place.Lokacija = request.Request.Lokacija;
            var result = await unitOfWork.CompleteAsync();
            if (!result)
                return new Result<Mesto>
                {
                    Errors = new List<string> { "Error in saving data" },
                    IsSucces = false
                };
            return new Result<Mesto>
            {
                Data = place
            };

        }
    }
}
