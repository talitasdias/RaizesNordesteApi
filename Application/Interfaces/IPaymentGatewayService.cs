using RaizesNordeste.API.Application.DTOs.PaymentGateway;

namespace RaizesNordeste.API.Application.Services
{
    public interface IPaymentGatewayService
    {
        Task<PaymentGatewayResponseDTO> ProcessPaymentAsync(decimal valor, int metodoPagamento);
    }
}