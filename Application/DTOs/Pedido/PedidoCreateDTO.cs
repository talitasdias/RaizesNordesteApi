using System.ComponentModel.DataAnnotations;

namespace RaizesNordeste.API.Application.DTOs.Pedido
{
    public class PedidoCreateDTO
    {
        [Required(ErrorMessage = "O UnidadeId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O UnidadeId deve ser maior que 0.")]
        public int UnidadeId { get; set; }

        [Required(ErrorMessage = "O CanalPedido é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O CanalPedido deve ser um valor válido.")]
        public int CanalPedido { get; set; }

        [MaxLength(500, ErrorMessage = "A observação não pode ultrapassar 500 caracteres.")]
        public string? Observacao { get; set; }

        [Required(ErrorMessage = "O pedido deve conter pelo menos um item.")]
        [MinLength(1, ErrorMessage = "O pedido deve conter pelo menos um item.")]
        public List<PedidoItemCreateDTO> Itens { get; set; } = new();
    }
}