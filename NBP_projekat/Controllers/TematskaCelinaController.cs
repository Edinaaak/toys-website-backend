using Microsoft.AspNetCore.Mvc;
using UmetnickaDela.Infrastructure;

namespace NBP_projekat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TematskaCelinaController : ControllerBase
    {

        private readonly IUnitOfWork unitOfWork;

        public TematskaCelinaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await unitOfWork.TematskaCelina.GetAll());
      
    }
}
