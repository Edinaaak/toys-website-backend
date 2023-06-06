using MediatR;
using NBP_projekat.Exceptions;
using UmetnickaDela.Contracts.Models.Identity.Response;
using UmetnickaDela.Contracts.Models;
using UmetnickaDela.Infrastructure;

namespace NBP_projekat.Mediator.Users
{
    public record UserGoogleLoginCommand(string email) : IRequest<Result<LoginResponse>>
    {
    }

    public class UserGoogleLoginHandler : IRequestHandler<UserGoogleLoginCommand, Result<LoginResponse>>
    {
        public IUnitOfWork unitOfWork;
        public UserGoogleLoginHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<LoginResponse>> Handle(UserGoogleLoginCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.email))
                throw new LoginCustomException("Input is required");
            var result = await unitOfWork.userRepository.googleLogin(request.email);
            if (result.error != "")
            {
                return new Result<LoginResponse>
                {
                    Errors = new List<string> { result.error },
                    IsSucces = false
                };

            }

            return new Result<LoginResponse>
            {
                Data = result
            };

        }
    }
}
