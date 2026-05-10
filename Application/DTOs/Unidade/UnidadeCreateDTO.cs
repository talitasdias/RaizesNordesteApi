using System.ComponentModel.DataAnnotations;

namespace RaizesNordeste.API.Application.DTOs.Unidade
{
    public class UnidadeCreateDTO
    {
        [Required(ErrorMessage = "O nome da unidade é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome não pode ultrapassar 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O endereço da unidade é obrigatório.")]
        [MaxLength(200, ErrorMessage = "O endereço não pode ultrapassar 200 caracteres.")]
        public string Endereco { get; set; } = string.Empty;

        public bool Ativa { get; set; } = true;
    }
}