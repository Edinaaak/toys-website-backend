using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UmetnickaDela.Contracts.Models;
using UmetnickaDela.Contracts.Models.Identity.Response;
using UmetnickaDela.Data.Models;

namespace NBP_projekat.Mediator.Places
{
    public record GetJuryListQuery : IRequest<Result<IEnumerable<UserResponse>>>
    {
    }

    public class GetJuryListHandler : IRequestHandler<GetJuryListQuery, Result<IEnumerable<UserResponse>>>
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        public GetJuryListHandler(UserManager<User> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<Result<IEnumerable<UserResponse>>> Handle(GetJuryListQuery request, CancellationToken cancellationToken)
        {
            var list = await userManager.GetUsersInRoleAsync("Moderator");
            var juryList = new List<User>();
            foreach(var user in list)
            {
                if (!user.EmailConfirmed)
                    juryList.Add(user);
            }
            var mapped = mapper.Map<List<UserResponse>>(juryList);
            if (juryList == null)
                return new Result<IEnumerable<UserResponse>>
                { Errors = new List<string> { "There are not juries" },
                    IsSucces = false
                
                };
            return new Result<IEnumerable<UserResponse>>
            {
                Data = mapped
            };
            
        }
    }
}
