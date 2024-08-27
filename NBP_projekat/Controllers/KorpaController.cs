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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await unitOfWork.KorpeRepository.DeleteKopra(id);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> ChangeQuantity(UpdateKorpaRequest updateKorpaRequest)
        {
            var result = await unitOfWork.KorpeRepository.ChangeQuantity(updateKorpaRequest.Id, updateKorpaRequest.Quantity);
            return Ok(result);
        }

        [HttpDelete("deleteFromCart/{id}")]
        public async Task<IActionResult> DeleteProductsFromCart(int id)
        {
            var result = await unitOfWork.KorpeRepository.IsprazniKorpu(id);
            return Ok(result);
        }
    }
}
