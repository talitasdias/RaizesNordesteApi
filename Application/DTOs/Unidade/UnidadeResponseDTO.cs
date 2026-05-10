namespace RaizesNordeste.API.Application.DTOs.Unidade
{
    public class UnidadeResponseDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Endereco { get; set; } = string.Empty;

        public bool Ativa { get; set; }
    }
}