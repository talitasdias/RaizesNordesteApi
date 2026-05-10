namespace RaizesNordeste.API.Application.DTOs.PaymentGateway
{
    public class PaymentGatewayResponseDTO
    {
        public bool Sucesso { get; set; }

        public string Status { get; set; } = string.Empty;

        public string TransactionId { get; set; } = string.Empty;

        public string Mensagem { get; set; } = string.Empty;
    }
}