using RaizesNordeste.API.Application.DTOs.PaymentGateway;

namespace RaizesNordeste.API.Application.Services
{
    public class MockPaymentGatewayService : IPaymentGatewayService
    {
        public async Task<PaymentGatewayResponseDTO> ProcessPaymentAsync(decimal valor, int metodoPagamento)
        {
            // Simula delay externo
            await Task.Delay(1000);

            bool aprovado;

            if (metodoPagamento == 1) // Pix sempre aprova
            {
                aprovado = true;
            }
            else if (metodoPagamento == 2) // Cartão: depende do valor
            {
                aprovado = valor <= 500;
            }
            else // outros métodos sempre recusam
            {
                aprovado = false;
            }

            return new PaymentGatewayResponseDTO
            {
                Sucesso = aprovado,
                Status = aprovado ? "APPROVED" : "DENIED",
                TransactionId = Guid.NewGuid().ToString(),
                Mensagem = aprovado ? "Pagamento aprovado" : "Pagamento recusado"
            };
        }
    }
}