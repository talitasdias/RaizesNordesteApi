using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaizesNordeste.API.Application.DTOs;
using RaizesNordeste.API.Application.Interfaces;

namespace RaizesNordeste.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutosController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] int pagina = 1, [FromQuery] int tamanhoPagina = 10)
        {
            try
            {
                var result = await _produtoService.GetAllAsync(pagina, tamanhoPagina);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao buscar produtos.");
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _produtoService.GetByIdAsync(id);

                if (result is null)
                    return NotFound($"Produto de Id {id} não encontrado.");

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao buscar produto.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Gerente,Admin")]
        public async Task<IActionResult> Create([FromBody] ProdutoCreateDTO produtoDto)
        {
            try
            {
                var result = await _produtoService.CreateAsync(produtoDto);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao criar produto.");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Gerente,Admin")]
        public async Task<IActionResult> Update([FromBody] ProdutoUpdateDTO produtoDto, int id)
        {
            try
            {
                if (produtoDto.Id != id)
                    return BadRequest("Ids informados não podem ser diferentes.");

                var result = await _produtoService.UpdateAsync(produtoDto, id);

                if (result is null)
                    return NotFound($"Produto de Id {id} não encontrado.");

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao atualizar produto.");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Gerente,Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deletado = await _produtoService.DeleteAsync(id);

                if (deletado)
                    return NoContent();

                return BadRequest($"Produto de Id {id} não foi deletado.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao deletar produto.");
            }
        }
    }
}