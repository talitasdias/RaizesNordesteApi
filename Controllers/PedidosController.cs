using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaizesNordeste.API.Application.DTOs.Pedido;
using RaizesNordeste.API.Application.Interfaces;
using RaizesNordeste.API.Domain.Enums;

namespace RaizesNordeste.API.Controllers
{
    [ApiController]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _service;

        public PedidosController(IPedidoService service)
        {
            _service = service;
        }

        [HttpGet("meus-pedidos")]
        [Authorize]
        public async Task<IActionResult> GetMyOrders(
            [FromQuery] int pagina = 1,
            [FromQuery] int tamanhoPagina = 10,
            [FromQuery] CanalPedido? canalPedido = null,
            [FromQuery] StatusPedido? statusPedido = null)
        {
            try
            {
                var usuarioIdClaim = User.Claims
                    .FirstOrDefault(x => x.Type.Contains("nameidentifier"));

                if (usuarioIdClaim == null)
                    return Unauthorized();

                var usuarioId = int.Parse(usuarioIdClaim.Value);

                var pedidos = await _service.GetByUsuarioIdAsync(
                    usuarioId, pagina, tamanhoPagina, canalPedido, statusPedido);

                return Ok(pedidos);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao buscar pedidos.");
            }
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create(PedidoCreateDTO dto)
        {
            try
            {
                var usuarioIdClaim = User.Claims
                    .FirstOrDefault(x => x.Type.Contains("nameidentifier"));

                if (usuarioIdClaim == null)
                    return Unauthorized();

                var usuarioId = int.Parse(usuarioIdClaim.Value);

                var pedido = await _service.CreateAsync(usuarioId, dto);

                return StatusCode(201, pedido);
            }
            catch (InvalidOperationException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao criar pedido.");
            }
        }

        [HttpPatch("{id}/status")]
        [Authorize(Roles = "Cozinha,Gerente,Admin")]
        public async Task<IActionResult> UpdateStatus(int id, PedidoUpdateStatusDTO dto)
        {
            try
            {
                var updated = await _service.UpdateStatusAsync(id, dto);

                if (!updated)
                    return NotFound("Pedido não encontrado.");

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao atualizar status do pedido.");
            }
        }

        [HttpPatch("{id}/cancelar")]
        [Authorize(Roles = "Cliente,Atendente,Gerente,Admin")]
        public async Task<IActionResult> Cancel(int id)
        {
            try
            {
                var cancelled = await _service.CancelAsync(id);

                if (!cancelled)
                    return NotFound("Pedido não encontrado.");

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao cancelar pedido.");
            }
        }
    }
}