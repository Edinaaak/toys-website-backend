using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using NBP_projekat.Mediator.Masterpieces;
using System.Diagnostics;
using UmetnickaDela.Contracts.Models.Masterpiece.Request;

namespace NBP_projekat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MasterpieceController : ControllerBase
    {
        private readonly IMediator mediator;
        public MasterpieceController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]MasterpieceFilterRequest request)
        {
            var result = await mediator.Send(new GetAllMasterpiecesQuery(request));
            if (!result.IsSucces)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await mediator.Send(new GetOneMasterpieceQuery(id));
            if(!result.IsSucces)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }
        [HttpPost]
        public async Task<IActionResult> createMasterPiece([FromForm] CreateMasterpieceRequesr request)
        {
            var result = await mediator.Send(new CreateMasterpieceCommand(request));
            if (!result.IsSucces)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await mediator.Send(new DeleteMasterpieceCommand(id));
            if(!result.IsSucces)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.IsSucces);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateMasterpieceRequest request)
        {
            var result = await mediator.Send(new UpdateMasterpieceCommand(id, request));
            if(!result.IsSucces)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }
    }
}
