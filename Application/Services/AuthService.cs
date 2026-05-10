using Microsoft.EntityFrameworkCore;
using RaizesNordeste.API.Application.DTOs;
using RaizesNordeste.API.Application.Interfaces;
using RaizesNordeste.API.Domain.Entities;
using RaizesNordeste.API.Domain.Enums;
using RaizesNordeste.API.Infrastructure.Persistence;
using RaizesNordeste.API.Infrastructure.Security;

namespace RaizesNordeste.API.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;

        private readonly TokenService _tokenService;

        public AuthService(
            AppDbContext context,
            TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task RegisterAsync(RegisterDTO dto)
        {
            if (await _context.Usuarios
                .AnyAsync(x => x.Email == dto.Email))
            {
                throw new Exception(
                    "Email já cadastrado.");
            }

            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                SenhaHash = BCrypt.Net.BCrypt
                    .HashPassword(dto.Senha),

                Role = Role.Cliente,

                AceitouProgramaFidelidade =
                    dto.AceitouProgramaFidelidade
            };

            _context.Usuarios.Add(usuario);

            await _context.SaveChangesAsync();
        }

        public async Task<string> LoginAsync(LoginDTO dto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(x =>
                    x.Email == dto.Email);

            if (usuario == null)
                throw new Exception(
                    "Email ou senha inválidos.");

            bool senhaOk = BCrypt.Net.BCrypt.Verify(
                dto.Senha,
                usuario.SenhaHash);

            if (!senhaOk)
                throw new Exception(
                    "Email ou senha inválidos.");

            return _tokenService.GenerateToken(usuario);
        }
    }
}