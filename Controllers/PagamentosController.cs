using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaizesNordeste.API.Application.DTOs.Pagamento;
using RaizesNordeste.API.Application.Interfaces;

namespace RaizesNordeste.API.Controllers
{
    [ApiController]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("api/[controller]")]
    public class PagamentosController : ControllerBase
    {
        private readonly IPagamentoService _service;

    public PagamentosController(IPagamentoService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = "Cliente,Atendente")]
    public async Task<IActionResult> ProcessarPagamento(
        PagamentoCreateDTO dto)
    {
            try
            {
                var pagamento = await _service.ProcessarPagamentoAsync(dto);

                return Ok(pagamento);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao buscar estoques da unidade.");
            }
        
    }
    }
}