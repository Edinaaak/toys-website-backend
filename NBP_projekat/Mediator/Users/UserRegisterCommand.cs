using MediatR;
using UmetnickaDela.Contracts.Models;
using UmetnickaDela.Contracts.Models.Identity.Request;
using UmetnickaDela.Contracts.Models.Identity.Response;
using UmetnickaDela.Infrastructure;

namespace NBP_projekat.Mediator.Users
{
    public record UserRegisterCommand(RegisterRequest request) : IRequest<Result<UserResponse>>
    {
    }

    public class UserRegisterHandler : IRequestHandler<UserRegisterCommand, Result<UserResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        public UserRegisterHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<UserResponse>> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            var result =  await unitOfWork.userRepository.register(request.request);
            if (!string.IsNullOrEmpty(result.Error))
                return new Result<UserResponse>
                {
                    Errors = new List<string> { result.Error },
                    IsSucces = false
                };
            if(result == null)
            {
                return new Result<UserResponse>
                { 
                    Errors = new List<string> { "Something went wrong, try again"},
                    IsSucces = false
                };

            }
            return new Result<UserResponse>
            {
                Data = result
            };
            
        }
    }
}
