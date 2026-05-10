using RaizesNordeste.API.Domain.Enums;

namespace RaizesNordeste.API.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string SenhaHash { get; set; } = string.Empty;

        public string? Telefone { get; set; }

        public Role Role { get; set; }

        public bool Ativo { get; set; } = true;

        public int PontosFidelidade { get; set; }

        public bool AceitouProgramaFidelidade { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    }
}