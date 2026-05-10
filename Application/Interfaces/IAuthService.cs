using RaizesNordeste.API.Application.DTOs;

namespace RaizesNordeste.API.Application.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDTO dto);

        Task<string> LoginAsync(LoginDTO dto);
    }
}