using AutoMapper;
using MediatR;
using NBP_projekat.ImageUploadMethod;
using UmetnickaDela.Contracts.Models;
using UmetnickaDela.Contracts.Models.Masterpiece.Request;
using UmetnickaDela.Contracts.Models.Masterpiece.Response;
using UmetnickaDela.Data.Models;
using UmetnickaDela.Infrastructure;

namespace NBP_projekat.Mediator.Masterpieces
{
    public record UpdateMasterpieceCommand (int id, UpdateMasterpieceRequest Request) : IRequest<Result<CreateMasterpieceResponse>>
    {
    }

    public class UpdateMasterpieceHandler : IRequestHandler<UpdateMasterpieceCommand, Result<CreateMasterpieceResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public UpdateMasterpieceHandler(IUnitOfWork unitOfWork, IMapper mapper, Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this._hostingEnvironment = _hostingEnvironment;
        }

        public async Task<Result<CreateMasterpieceResponse>> Handle(UpdateMasterpieceCommand request, CancellationToken cancellationToken)
        {
            var mpiece = await unitOfWork.UmetnickoDelo.getById(request.id);
            if(mpiece == null)
            {
                return new Result<CreateMasterpieceResponse>
                {
                    Errors = new List<string> { $"can not find masterpiece with {request.id}" },
                    IsSucces = false
                };
            }


            mapper.Map<UpdateMasterpieceRequest, UmetnickoDelo>(request.Request, mpiece);
            mpiece.Putanja = await Upload.SaveFile(_hostingEnvironment.ContentRootPath, request.Request.Putanja, "images");
            var result = await unitOfWork.CompleteAsync();
            var mappedPiece = mapper.Map<CreateMasterpieceResponse>(mpiece);
            if (!result)
                return new Result<CreateMasterpieceResponse>
                {
                    Errors = new List<string> { "Can not save this change" },
                    IsSucces = false
                };
            return new Result<CreateMasterpieceResponse>
            {
                Data = mappedPiece
            };
        }
    }
}
