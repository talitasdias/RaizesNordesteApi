namespace RaizesNordeste.API.Domain.Entities
{
    public class Estoque
    {
        public int Id { get; set; }

        public int ProdutoId { get; set; }

        public Produto Produto { get; set; } = null!;

        public int UnidadeId { get; set; }

        public Unidade Unidade { get; set; } = null!;

        public int Quantidade { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    }
}