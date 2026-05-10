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
            try
            {
                var result = await _service.GetAllAsync();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao buscar unidades.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _service.GetById(id);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao buscar unidade.");
            }
        }

        [HttpGet("{id}/cardapio")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCardapio(int id)
        {
            try
            {
                var cardapio = await _service.GetCardapioAsync(id);
                return Ok(cardapio);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao buscar cardápio da unidade.");
            }
        }

        [HttpPost]
        [Authorize("Admin")]
        public async Task<IActionResult> Create([FromBody] UnidadeCreateDTO unidadeDto)
        {
            try
            {
                var result = await _service.Create(unidadeDto);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao criar unidade.");
            }
        }
    }
}