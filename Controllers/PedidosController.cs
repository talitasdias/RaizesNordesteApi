using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaizesNordeste.API.Application.DTOs.Pedido;
using RaizesNordeste.API.Application.Interfaces;

namespace RaizesNordeste.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _service;

        public PedidosController(IPedidoService service)
        {
            _service = service;
        }

        [HttpGet("meus-pedidos")]
        public async Task<IActionResult> GetMyOrders()
        {
            var usuarioIdClaim = User.Claims
                .FirstOrDefault(x => x.Type.Contains("nameidentifier"));

            if (usuarioIdClaim == null)
                return Unauthorized();

            var usuarioId = int.Parse(usuarioIdClaim.Value);

            var pedidos = await _service.GetByUsuarioIdAsync(usuarioId);

            return Ok(pedidos);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PedidoCreateDTO dto)
        {
            var usuarioIdClaim = User.Claims
                .FirstOrDefault(x => x.Type.Contains("nameidentifier"));

            if (usuarioIdClaim == null)
                return Unauthorized();

            var usuarioId = int.Parse(usuarioIdClaim.Value);

            var pedido = await _service.CreateAsync(usuarioId, dto);

            return Ok(pedido);
        }

        [HttpPatch("{id}/status")]
        //[Authorize(Roles = "Admin,Gerente")]
        public async Task<IActionResult> UpdateStatus(int id, PedidoUpdateStatusDTO dto)
        {
            var updated = await _service.UpdateStatusAsync(id, dto);

            if (!updated)
                return NotFound("Pedido não encontrado.");

            return NoContent();
        }

        [HttpPatch("{id}/cancelar")]
        public async Task<IActionResult> Cancel(int id)
        {
            var cancelled = await _service.CancelAsync(id);

            if (!cancelled)
                return NotFound("Pedido não encontrado.");

            return NoContent();
        }
    }
}