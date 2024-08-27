using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UmetnickaDela.Contracts.Models.Comments.Request;
using UmetnickaDela.Data.Models;
using UmetnickaDela.Infrastructure;

namespace NBP_projekat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public CommentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            var result = await unitOfWork.CommentRepository.getById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCommentRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request cannot be null.");
            }

            if (unitOfWork?.CommentRepository == null)
            {
                return StatusCode(500, "Internal server error: CommentRepository is not available.");
            }

            try
            {
                await unitOfWork.CommentRepository.AddComment(request);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception details
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpGet("allComments/{id}")]
        public async Task<IActionResult> GetAllComments(int id)
        {
            var result = await unitOfWork.CommentRepository.GetCommentsWithInclude(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var result = await unitOfWork.CommentRepository.DeleteComment(id);
            if (result)
                return Ok();
            return BadRequest();
        }
    }
}
