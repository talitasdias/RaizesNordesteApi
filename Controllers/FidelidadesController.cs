using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaizesNordeste.API.Application.DTOs;
using RaizesNordeste.API.Infrastructure.Persistence;

namespace RaizesNordeste.API.Controllers
{
    [ApiController]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("api/[controller]")]
    public class FidelidadesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FidelidadesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("pontos")]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> GetPontos()
        {
            try
            {
                var usuarioId = int.Parse(
                    User.FindFirst(ClaimTypes.NameIdentifier)!
                        .Value);

                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.Id == usuarioId);

                if (usuario == null)
                    return NotFound("Usuário não encontrado.");

                return Ok(new FidelidadePontosDTO
                {
                    Pontos = usuario.PontosFidelidade
                });
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao buscar pontos de fidelidade.");
            }
        }
    }
}