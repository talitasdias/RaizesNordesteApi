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
        public async Task<IActionResult> GetAll()
        {
            var result = await _produtoService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _produtoService.GetByIdAsync(id);
            if (result is null)
                return NotFound($"Produto de Id {id} não encontrado.");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProdutoCreateDTO produtoDto)
        {
            var result = await _produtoService.CreateAsync(produtoDto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] ProdutoUpdateDTO produtoDto, int id)
        {
            if (produtoDto.Id != id)
                return BadRequest("Ids informados não podem ser diferentes.");
            var result = await _produtoService.UpdateAsync(produtoDto, id);
            if (result is null)
                return NotFound($"Produto de Id {id} não encontrado.");
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletado = await _produtoService.DeleteAsync(id);

            if (deletado)
                return NoContent();
            return BadRequest($"Produto de Id {id} não foi deletado.");
        }
    }
}