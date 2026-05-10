using System.ComponentModel.DataAnnotations;

namespace RaizesNordeste.API.Application.DTOs.Pedido
{
    public class PedidoUpdateStatusDTO
    {
        [Required(ErrorMessage = "O Status é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O Status deve ser um valor válido.")]
        public int Status { get; set; }
    }
}