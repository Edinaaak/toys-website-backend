using MediatR;
using Microsoft.AspNetCore.Mvc;
using NBP_projekat.Mediator.Places;
using UmetnickaDela.Contracts.Models.Mesto.Request;

namespace NBP_projekat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaceController : ControllerBase
    {
        private readonly IMediator mediator;
        public PlaceController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePlace (CreatePlaceRequest request)
        {
            var result = await mediator.Send(new CreatePlaceCommand(request));
            if (!result.IsSucces)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await mediator.Send(new GetAllPlacesQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await mediator.Send(new GetPlaceQuery(id));
            if (!result.IsSucces)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlace(int id)
        {
            var result = await mediator.Send(new RemovePlaceCommand(id));
            if(!result.IsSucces)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.IsSucces);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlace(int id, CreatePlaceRequest request)
        {
            var result = await mediator.Send(new UpdatePlaceCommand(id, request));
            if(!result.IsSucces)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }


    }
}
