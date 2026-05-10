using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaizesNordeste.API.Application.DTOs;
using RaizesNordeste.API.Infrastructure.Persistence;

namespace RaizesNordeste.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FidelidadesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FidelidadesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("pontos")]
        public async Task<IActionResult> GetPontos()
        {
            var usuarioId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!
                    .Value);

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(x => x.Id == usuarioId);

            if (usuario == null)
                return NotFound();

            return Ok(new FidelidadePontosDTO
            {
                Pontos = usuario.PontosFidelidade
            });
        }
    }
}