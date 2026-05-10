using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaizesNordeste.API.Application.DTOs.Estoque;
using RaizesNordeste.API.Application.Interfaces;

namespace RaizesNordeste.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstoquesController : ControllerBase
    {
        private readonly IEstoqueService _service;

        public EstoquesController(IEstoqueService service)
        {
            _service = service;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin,Gerente")]
        public async Task<IActionResult> GetAll()
        {
            var estoques = await _service.GetAllAsync();

            return Ok(estoques);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Gerente")]
        public async Task<IActionResult> GetById(int id)
        {
            var estoque = await _service.GetByIdAsync(id);

            if (estoque == null)
                return NotFound("Estoque não encontrado.");

            return Ok(estoque);
        }

        [HttpGet("unidade/{id}")]
        //[Authorize(Roles = "Admin,Gerente")]
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
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Gerente")]
        public async Task<IActionResult> Create(EstoqueCreateDTO dto)
        {
            var estoque = await _service.CreateAsync(dto);

            return Ok(estoque);
        }

        [HttpPatch("{id}/quantidade")]
        [Authorize(Roles = "Admin,Gerente")]
        public async Task<IActionResult> UpdateQuantidade(
            int id,
            EstoqueUpdateQuantidadeDTO dto)
        {
            var updated = await _service
                .UpdateQuantidadeAsync(id, dto);

            if (!updated)
                return NotFound("Estoque não encontrado.");

            return NoContent();
        }
    }
}