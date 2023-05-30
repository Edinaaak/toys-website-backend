using MediatR;
using Microsoft.AspNetCore.Identity;
using UmetnickaDela.Contracts.Models;
using UmetnickaDela.Contracts.Models.Identity.Response;
using UmetnickaDela.Data.Models;
using UmetnickaDela.Infrastructure;

namespace NBP_projekat.Mediator.Users
{
    public record DeleteUserCommand (int id) : IRequest<Result<bool>>
    {
    }

    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Result<bool>>
    {
        private readonly UserManager<User> userManager;
        private readonly IUnitOfWork unitOfWork;
        public DeleteUserHandler(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.id.ToString());
            if (user == null)
            {
                return new Result<bool>
                {
                    Errors = new List<string> { $"User with {request.id} is not found" },
                    IsSucces = false
                };
            }
            await userManager.DeleteAsync(user);
            await unitOfWork.CompleteAsync();
            return new Result<bool>
            {
                IsSucces = true
            };
        }
    }
}
