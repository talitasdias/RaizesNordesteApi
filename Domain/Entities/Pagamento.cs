using RaizesNordeste.API.Domain.Enums;

namespace RaizesNordeste.API.Domain.Entities
{
    public class Pagamento
    {
        public int Id { get; set; }

        public int PedidoId { get; set; }

        public Pedido Pedido { get; set; } = null!;

        public decimal Valor { get; set; }

        public MetodoPagamento Metodo { get; set; }

        public StatusPagamento Status { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    }
}