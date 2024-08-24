using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UmetnickaDela.Contracts.Models.Rasprodaja.Requests;
using UmetnickaDela.Data.Models;
using UmetnickaDela.Infrastructure;

namespace NBP_projekat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RasprodajaController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public RasprodajaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
                this.unitOfWork = unitOfWork;
                this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await unitOfWork.RasprodajaRepository.GetRasprodajaWithInclude());


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await unitOfWork.RasprodajaRepository.getById(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> CreateRasprodaja(RasprodajaRequestCreateDTO request)
        {
            var rasprodajaToAdd = mapper.Map<Rasprodaja>(request);
            await unitOfWork.RasprodajaRepository.Add(rasprodajaToAdd);
            await unitOfWork.CompleteAsync();
            return Ok();
        }
    }
}
