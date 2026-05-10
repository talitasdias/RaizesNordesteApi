using System.ComponentModel.DataAnnotations;

namespace RaizesNordeste.API.Application.DTOs.Estoque
{
    public class EstoqueCreateDTO
    {
        [Required(ErrorMessage = "O ProdutoId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O ProdutoId deve ser maior que 0.")]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "O UnidadeId é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O UnidadeId deve ser maior que 0.")]
        public int UnidadeId { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade não pode ser negativa.")]
        public int Quantidade { get; set; }
    }
}