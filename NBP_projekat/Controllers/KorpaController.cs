using Microsoft.AspNetCore.Mvc;
using UmetnickaDela.Contracts.Models.Korpa.Request;
using UmetnickaDela.Infrastructure;
using UmetnickaDela.Infrastructure.Interfaces;

namespace NBP_projekat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KorpaController : ControllerBase
    {
       private readonly IUnitOfWork unitOfWork;
        public KorpaController(IUnitOfWork unitOfWork)
        {
              this.unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            var result = await unitOfWork.KorpeRepository.GetKorpaWithIncludeByUserId(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddKorpaRequest request)
        {
            var result = await unitOfWork.KorpeRepository.AddProizvodToKorpa(request);
            return Ok(result);
        }
    }
}
