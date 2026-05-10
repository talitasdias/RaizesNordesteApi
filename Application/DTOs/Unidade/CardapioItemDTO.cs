namespace RaizesNordeste.API.Application.DTOs
{
    public class CardapioItemDTO
    {
        public int ProdutoId { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public decimal Preco { get; set; }

        public int QuantidadeDisponivel { get; set; }
    }
}