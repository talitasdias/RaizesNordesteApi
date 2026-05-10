using System.ComponentModel.DataAnnotations;

namespace RaizesNordeste.API.Application.DTOs.Pagamento
{
    public class PagamentoCreateDTO
    {
        [Required(ErrorMessage = "O PedidoId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O PedidoId deve ser maior que 0.")]
        public int PedidoId { get; set; }

        [Required(ErrorMessage = "O MetodoPagamento é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O MetodoPagamento deve ser um valor válido.")]
        public int MetodoPagamento { get; set; }
    }
}