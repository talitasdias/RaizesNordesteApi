namespace RaizesNordeste.API.Application.DTOs.Pedido
{
    public class PedidoResponseDTO
    {
        public int Id { get; set; }

        public decimal ValorTotal { get; set; }

        public string StatusPedido { get; set; } = string.Empty;

        public string CanalPedido { get; set; } = string.Empty;

        public DateTime DataCriacao { get; set; }

        public List<PedidoItemResponseDTO> Itens { get; set; } = [];
    }
}