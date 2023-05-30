using AutoMapper;
using MediatR;
using NBP_projekat.ImageUploadMethod;
using UmetnickaDela.Contracts.Models;
using UmetnickaDela.Contracts.Models.Masterpiece.Request;
using UmetnickaDela.Contracts.Models.Masterpiece.Response;
using UmetnickaDela.Data.Models;
using UmetnickaDela.Infrastructure;
using NBP_projekat.Extensions;

namespace NBP_projekat.Mediator.Masterpieces
{
    public record CreateMasterpieceCommand(CreateMasterpieceRequesr request) : IRequest<Result<CreateMasterpieceResponse>>
    {
    }

    public class CreateMasterpieceHandler : IRequestHandler<CreateMasterpieceCommand, Result<CreateMasterpieceResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public CreateMasterpieceHandler(IUnitOfWork unitOfWork, IMapper mapper, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<Result<CreateMasterpieceResponse>> Handle(CreateMasterpieceCommand request, CancellationToken cancellationToken)
        {
            var delo = mapper.Map<UmetnickoDelo>(request.request);
            delo.Putanja = await Upload.SaveFile(_hostingEnvironment.ContentRootPath, request.request.Putanja, "images");
            await unitOfWork.UmetnickoDelo.Add(delo);
            var result = await unitOfWork.CompleteAsync();
            if(!result)
                return new Result<CreateMasterpieceResponse>
                { Errors = new List<string> { "Can not create"},
                    IsSucces = false };
            var response = mapper.Map<CreateMasterpieceResponse>(delo);
            return new Result<CreateMasterpieceResponse>
            {
                Data = response
            };

        }
    }
}
