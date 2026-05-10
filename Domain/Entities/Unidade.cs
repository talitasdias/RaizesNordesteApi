namespace RaizesNordeste.API.Domain.Entities
{
    public class Unidade
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Endereco { get; set; } = string.Empty;

        public bool Ativa { get; set; } = true;
    }
}