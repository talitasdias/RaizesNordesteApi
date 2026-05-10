using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaizesNordeste.API.Application.DTOs.Unidade;
using RaizesNordeste.API.Application.Interfaces;

namespace RaizesNordeste.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnidadesController : ControllerBase
    {
        private readonly IUnidadeService _service;
        public UnidadesController(IUnidadeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetById(id);
            if (result is null)
                return NotFound($"Unidade de Id {id} não encontrado.");
            return Ok(result);
        }

        [HttpGet("{id}/cardapio")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCardapio(int id)
        {
            var cardapio = await _service.GetCardapioAsync(id);

            return Ok(cardapio);
        }

        [HttpPost]
        [Authorize("Admin")]
        public async Task<IActionResult> Create([FromBody] UnidadeCreateDTO unidadeDto)
        {
            var result = await _service.Create(unidadeDto);
            return Ok(result);
        }
    }
}