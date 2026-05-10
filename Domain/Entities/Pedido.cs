using RaizesNordeste.API.Domain.Enums;

namespace RaizesNordeste.API.Domain.Entities
{
    public class Pedido
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; } = null!;

        public int UnidadeId { get; set; }

        public Unidade Unidade { get; set; } = null!;

        public CanalPedido CanalPedido { get; set; }

        public StatusPedido StatusPedido { get; set; }

        public decimal ValorTotal { get; set; }

        public string? Observacao { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        public List<ItemPedido> Itens { get; set; } = new();
    }
}