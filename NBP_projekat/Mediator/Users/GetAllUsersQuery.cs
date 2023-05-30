using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UmetnickaDela.Contracts.Models;
using UmetnickaDela.Contracts.Models.Identity.Response;
using UmetnickaDela.Data.Models;

namespace NBP_projekat.Mediator.Users
{
    public record GetAllUsersQuery : IRequest<Result<IEnumerable<UserResponse>>> { }

    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, Result<IEnumerable<UserResponse>>>
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        public GetAllUsersHandler(UserManager<User> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<Result<IEnumerable<UserResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var lista = await userManager.Users.ToListAsync();
            var mappedList = mapper.Map<List<UserResponse>>(lista);
            return new Result<IEnumerable<UserResponse>>
            {
                Data = mappedList

            };
        }
    }
}
