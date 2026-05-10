using Microsoft.AspNetCore.Mvc;
using RaizesNordeste.API.Application.DTOs.Pagamento;
using RaizesNordeste.API.Application.Interfaces;

namespace RaizesNordeste.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagamentosController : ControllerBase
    {
        private readonly IPagamentoService _service;

    public PagamentosController(IPagamentoService service)
    {
        _service = service;
    }

    [HttpPost]
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