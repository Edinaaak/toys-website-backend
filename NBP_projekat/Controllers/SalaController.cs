using MediatR;
using Microsoft.AspNetCore.Mvc;
using NBP_projekat.Mediator.Sales;
using UmetnickaDela.Contracts.Models.Sala.Request;

namespace NBP_projekat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalaController : ControllerBase
    {
        private readonly IMediator mediator;

        public SalaController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await mediator.Send(new GetAllSalaQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await mediator.Send(new GetSalaQuery(id));
            if (!result.IsSucces)
                return NotFound(result.Errors.FirstOrDefault());
            return Ok(result.Data);

        }
        [HttpPost]
        public async Task<IActionResult> CreateSala(CreateSalaRequest request)
        {
            var result = await mediator.Send(new CreateSalaCommand(request));
            if (!result.IsSucces)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSala(int id)
        {
            var result = await mediator.Send(new RemoveSalaCommand(id));
            if(!result.IsSucces)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.IsSucces);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSala (int id, UpdateSalaRequest request )
        {
            var result = await mediator.Send(new UpdateSalaCommand(id, request.Naziv));
            if(!result.IsSucces)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

    }
}
