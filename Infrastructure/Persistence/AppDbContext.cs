using Microsoft.EntityFrameworkCore;
using RaizesNordeste.API.Domain.Entities;

namespace RaizesNordeste.API.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {            
        }

        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Produto> Produtos => Set<Produto>();
        public DbSet<Unidade> Unidades => Set<Unidade>();
        public DbSet<Pedido> Pedidos => Set<Pedido>();
        public DbSet<ItemPedido> ItensPedido => Set<ItemPedido>();
        public DbSet<Pagamento> Pagamentos => Set<Pagamento>();
        public DbSet<Estoque> Estoques => Set<Estoque>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
            .HasIndex(x => x.Email)
            .IsUnique();

            modelBuilder.Entity<Usuario>()
            .Property(x => x.Nome)
            .HasMaxLength(150)
            .IsRequired();

            modelBuilder.Entity<Produto>()
            .Property(x => x.Preco)
            .HasColumnType("numeric(10,2)");

            modelBuilder.Entity<Pedido>()
            .Property(x => x.ValorTotal)
            .HasColumnType("numeric(10,2)");

            modelBuilder.Entity<ItemPedido>()
            .Property(x => x.PrecoUnitario)
            .HasColumnType("numeric(10,2)");

            modelBuilder.Entity<ItemPedido>()
            .Property(x => x.Subtotal)
            .HasColumnType("numeric(10,2)");

            modelBuilder.Entity<Pagamento>()
            .Property(x => x.Valor)
            .HasColumnType("numeric(10,2)");

            modelBuilder.Entity<Estoque>()
            .HasIndex(x => new { x.ProdutoId, x.UnidadeId })
            .IsUnique();
        }
    }
}