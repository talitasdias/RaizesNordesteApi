namespace RaizesNordeste.API.Application.DTOs.Estoque
{
    public class EstoqueResponseDTO
    {
        public int Id { get; set; }

        public string Produto { get; set; } = string.Empty;

        public string Unidade { get; set; } = string.Empty;

        public int Quantidade { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}