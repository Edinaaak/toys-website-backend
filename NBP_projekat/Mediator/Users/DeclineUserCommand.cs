using MediatR;
using UmetnickaDela.Contracts.Models;
using UmetnickaDela.Infrastructure;

namespace NBP_projekat.Mediator.Users
{
    public record DeclineUserCommand (int id) : IRequest<Result<bool>>
    {
    }

    public class DeclineUserHandler : IRequestHandler<DeclineUserCommand, Result<bool>>
    {
        private readonly IUnitOfWork unitOfWork;
        public DeclineUserHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeclineUserCommand request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.userRepository.declineJury(request.id);
            if (!user)
                return new Result<bool>
                {
                    Errors = new List<string> { $"User with {request.id} does not exist" },
                    IsSucces = false
                };
            return new Result<bool>
            { IsSucces = true };
        }
    }
}
