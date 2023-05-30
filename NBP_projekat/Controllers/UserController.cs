using MediatR;
using Microsoft.AspNetCore.Mvc;
using NBP_projekat.Mediator.Users;
using UmetnickaDela.Contracts.Models.Identity.Request;

namespace NBP_projekat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;
        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers() => Ok(await mediator.Send(new GetAllUsersQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var result = await mediator.Send(new GetUserQuery(id));
            if(!result.IsSucces)
                return NotFound(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser (int id)
        {
            var result = await mediator.Send(new DeleteUserCommand(id));
            if(!result.IsSucces)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.IsSucces);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserRequest request)
        {
            var result = await mediator.Send(new UpdateUserCommand (id, request));
            if(!result.IsSucces)
                return NotFound(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }
             
    }
}
