using System.ComponentModel.DataAnnotations;

namespace RaizesNordeste.API.Application.DTOs.Estoque
{
    public class EstoqueUpdateQuantidadeDTO
    {
        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade não pode ser negativa.")]
        public int Quantidade { get; set; }
    }
}