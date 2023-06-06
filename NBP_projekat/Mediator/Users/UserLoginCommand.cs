using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NBP_projekat.Exceptions;
using UmetnickaDela.Contracts.Models;
using UmetnickaDela.Contracts.Models.Identity.Request;
using UmetnickaDela.Contracts.Models.Identity.Response;
using UmetnickaDela.Infrastructure;

namespace NBP_projekat.Mediator.Users
{
    public record UserLoginCommand (LoginRequest Request) : IRequest<Result<LoginResponse>>
    {
    }

    public class UserLoginHandler : IRequestHandler<UserLoginCommand, Result<LoginResponse>>
    {
        public IUnitOfWork unitOfWork;
        public UserLoginHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<LoginResponse>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Request.Email) || string.IsNullOrEmpty(request.Request.Password))
                throw new LoginCustomException("Both inputs are required");
            var result = await unitOfWork.userRepository.login(request.Request);
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
                Data= result
            };

        }
    }
}
