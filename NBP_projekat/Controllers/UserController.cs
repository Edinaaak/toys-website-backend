using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NBP_projekat.Mediator.Places;
using NBP_projekat.Mediator.Users;
using UmetnickaDela.Contracts.Models.Identity.Request;
using UmetnickaDela.Data.Models;

namespace NBP_projekat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly RoleManager<AppRole> role;
        public UserController(IMediator mediator, RoleManager<AppRole> role)
        {
            this.mediator = mediator;
            this.role = role;
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
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserRequest request)
        {
            var result = await mediator.Send(new UpdateUserCommand (id, request));
            if(!result.IsSucces)
                return NotFound(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

        [HttpGet("/role")]
        public async Task<IActionResult> getRoles()
        {
            User user = new User();
            var lista = await role.Roles.ToListAsync();
            return Ok(lista);

        }

        [HttpGet("jury-list")]
        public async Task<IActionResult> getJuries()
        {
            var result = await mediator.Send(new GetJuryListQuery());
            if(!result.IsSucces)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

        [HttpPut("accept/{id}")]
        public async Task<IActionResult> acceptJury(int id)
        {
            var result = await mediator.Send( new AcceptUserCommand(id));
            if(!result.IsSucces)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.IsSucces);
        }

        [HttpDelete("decline/{id}")]
        public async Task<IActionResult> declineJury(int id)
        {
            var result = await mediator.Send( new DeclineUserCommand (id));
            if(!result.IsSucces)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.IsSucces);
        }
             
    }
}
