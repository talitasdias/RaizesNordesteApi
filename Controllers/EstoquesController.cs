using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaizesNordeste.API.Application.DTOs.Estoque;
using RaizesNordeste.API.Application.Interfaces;

namespace RaizesNordeste.API.Controllers
{
    [ApiController]
    [Authorize(Roles = "Gerente,Admin")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("api/[controller]")]
    public class EstoquesController : ControllerBase
    {
        private readonly IEstoqueService _service;

        public EstoquesController(IEstoqueService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pagina = 1, [FromQuery] int tamanhoPagina = 10)
        {
            try
            {
                var estoques = await _service.GetAllAsync(pagina, tamanhoPagina);
                return Ok(estoques);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao buscar estoques.");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var estoque = await _service.GetByIdAsync(id);

                return Ok(estoque);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao buscar estoque.");
            }
        }

        [HttpGet("unidade/{id}")]
        public async Task<IActionResult> GetByIdUnidade(int id)
        {
            try
            {
                var estoques = await _service.GetByIdUnidadeAsync(id);
                return Ok(estoques);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao buscar estoques da unidade.");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create(EstoqueCreateDTO dto)
        {
            try
            {
                var estoque = await _service.CreateAsync(dto);
                return Ok(estoque);
            }            
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao criar estoque.");
            }
        }

        [HttpPatch("{id}/quantidade")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateQuantidade(int id, EstoqueUpdateQuantidadeDTO dto)
        {
            try
            {
                var updated = await _service.UpdateQuantidadeAsync(id, dto);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao atualizar quantidade do estoque.");
            }
        }
    }
}