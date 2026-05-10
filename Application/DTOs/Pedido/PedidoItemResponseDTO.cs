namespace RaizesNordeste.API.Application.DTOs.Pedido
{
    public class PedidoItemResponseDTO
    {
        public string Produto { get; set; } = string.Empty;

        public int Quantidade { get; set; }

        public decimal PrecoUnitario { get; set; }

        public decimal Subtotal { get; set; }
    }
}