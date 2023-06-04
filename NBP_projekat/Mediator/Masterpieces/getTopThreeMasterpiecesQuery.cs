using AutoMapper;
using MediatR;
using UmetnickaDela.Contracts.Models;
using UmetnickaDela.Contracts.Models.Masterpiece.Response;
using UmetnickaDela.Data.Migrations;
using UmetnickaDela.Infrastructure;

namespace NBP_projekat.Mediator.Masterpieces
{
    record class getTopThreeMasterpiecesQuery : IRequest<Result<List<getTop3Response>>>
    {
    }

    public class getTopThreeMasterpieceCommand : IRequestHandler<getTopThreeMasterpiecesQuery, Result<List<getTop3Response>>>
        {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public getTopThreeMasterpieceCommand(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;

        }
        async Task<Result<List<getTop3Response>>> IRequestHandler<getTopThreeMasterpiecesQuery, Result<List<getTop3Response>>>.Handle(getTopThreeMasterpiecesQuery request, CancellationToken cancellationToken)
        {
            var lista = await unitOfWork.UmetnickoDelo.GetAll();
            var list = new List<getTop3Response>();
            foreach(var item in lista)
            {
                float ocena = 0;
                try
                {
                     ocena = await unitOfWork.UmetnickoDelo.AverageRating(item.Id);
                }
                catch (Exception ex)
                {
                    ocena = 0;
                }
                list.Add(new getTop3Response
                {
                    Id = item.Id,
                    Name = item.Naziv,
                    Price = ocena,
                    Putanja = item.Putanja
                });
            }

            var sortedList = list.OrderByDescending(x => x.Price).Take(3).ToList();
            return new Result<List<getTop3Response>>
            { Data = sortedList};



        }
    }

}
