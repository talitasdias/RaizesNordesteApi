namespace RaizesNordeste.API.Application.DTOs.Pagamento
{
    public class PagamentoResponseDTO
    {
        public int PagamentoId { get; set; }

        public int PedidoId { get; set; }

        public string StatusPagamento { get; set; } = string.Empty;

        public decimal Valor { get; set; }
    }
}