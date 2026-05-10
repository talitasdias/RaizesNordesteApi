using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaizesNordeste.API.Application.DTOs;
using RaizesNordeste.API.Application.Interfaces;

namespace RaizesNordeste.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterDTO dto)
    {
        await _service.RegisterAsync(dto);

        return Ok("Usuário cadastrado.");
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginDTO dto)
    {
        var token = await _service.LoginAsync(dto);

        return Ok(new { token });
    }
}