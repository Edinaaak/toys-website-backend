﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using NBP_projekat.Exceptions;
using NBP_projekat.Mediator.Masterpieces;
using System.Diagnostics;
using UmetnickaDela.Contracts.Models.Masterpiece.Request;
using UmetnickaDela.Data;
using UmetnickaDela.Data.Models;
using UmetnickaDela.Infrastructure;

namespace NBP_projekat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MasterpieceController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IUnitOfWork unitOfWork;
        private readonly DataContext context;
        public MasterpieceController(IMediator mediator, IUnitOfWork unitOfWork, DataContext context)
        {
            this.mediator = mediator;
            this.unitOfWork = unitOfWork;
            this.context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]MasterpieceFilterRequest request)
        {
            var result = await mediator.Send(new GetAllMasterpiecesQuery(request));
            if (!result.IsSucces)
                throw new MasterpieceCustomException(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await mediator.Send(new GetOneMasterpieceQuery(id));
            if(!result.IsSucces)
                throw new MasterpieceCustomException(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }
        [HttpPost]
        public async Task<IActionResult> createMasterPiece([FromForm] CreateMasterpieceRequesr request)
        {
            var result = await mediator.Send(new CreateMasterpieceCommand(request));
            if (!result.IsSucces)
                throw new MasterpieceCustomException(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await mediator.Send(new DeleteMasterpieceCommand(id));
            if(!result.IsSucces)
                throw new MasterpieceCustomException(result.Errors.FirstOrDefault());
            return Ok(result.IsSucces);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateMasterpieceRequest request)
        {
            var result = await mediator.Send(new UpdateMasterpieceCommand(id, request));
            if(!result.IsSucces)
                throw new MasterpieceCustomException(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

        [HttpPost("add-mark")]
        public async Task<IActionResult> RateMasterpiece (AddMarkRequest userDelo)
        {
            var result = await unitOfWork.UmetnickoDelo.AddMark(userDelo);
            if(!result)
                throw new MasterpieceCustomException("You already rated this art painting");
            return Ok(userDelo.Ocena);
        }

        [HttpGet("thematic-unit")]
        public async Task<IActionResult> GetThematicUnits()
        {
            return Ok(await context.celine.ToListAsync());
        }

        [HttpGet("get-mark/{id}")]
        public async Task<IActionResult> getMark(int id)
        {
            var result = await unitOfWork.UmetnickoDelo.GetMark(id);
            if (result == null)
                throw new MasterpieceCustomException("You did not rate art paintings"); 
            return Ok(result);

        }

        [HttpGet("top-3")]
        public async Task<IActionResult> getTopThree()
        {
            var result = await mediator.Send( new getTopThreeMasterpiecesQuery() );
            return Ok(result.Data);
        }

        [HttpPut("update-masterpiece-auditorium")]
        public async Task<IActionResult> updateAuditorium([FromBody] addSalaRequest request)
        {
            var result = await mediator.Send(new UpdateSalaCommand(request.Id, request.IdSala));
            if (!result.IsSucces)
                throw new MasterpieceCustomException(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

        [HttpGet("get-reviews/{id}")]
        public async Task<IActionResult> getReviews(int id)
        {
            var result = await unitOfWork.UmetnickoDelo.GetReviews(id);
            return Ok(result);
        }
       
    }
}
