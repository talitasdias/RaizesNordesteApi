using Microsoft.EntityFrameworkCore;
using RaizesNordeste.API.Application.DTOs;
using RaizesNordeste.API.Domain.Entities;
using RaizesNordeste.API.Domain.Interfaces;
using RaizesNordeste.API.Infrastructure.Persistence;

namespace RaizesNordeste.API.Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _dbContext;

        public ProdutoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            return await _dbContext.Produtos.AsNoTracking().ToListAsync();
        }

        public async Task<Produto?> GetByIdAsync(int id)
        {
            return await _dbContext.Produtos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Produto> CreateAsync(ProdutoCreateDTO produtoDto)
        {
            var produtoEntity = new Produto
            {
                Nome = produtoDto.Nome,
                Descricao = produtoDto.Descricao,
                Disponivel = produtoDto.Disponivel,
                Preco = produtoDto.Preco
            };

            _dbContext.Produtos.Add(produtoEntity);
            await _dbContext.SaveChangesAsync();

            return produtoEntity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var produtoEntity = await GetByIdAsync(id);
            if (produtoEntity is null)
                return false;

            _dbContext.Produtos.Remove(produtoEntity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Produto?> UpdateAsync(ProdutoUpdateDTO produtoDto, int id)
        {
            var produtoBanco = await GetByIdAsync(id);
            if (produtoBanco is null)
                return null;
            
            produtoBanco.Nome = produtoDto.Nome;
            produtoBanco.Descricao = produtoDto.Descricao;
            produtoBanco.Preco = produtoDto.Preco;
            produtoBanco.Disponivel = produtoDto.Disponivel;
            
            _dbContext.Produtos.Update(produtoBanco);
            await _dbContext.SaveChangesAsync();

            return produtoBanco;
        }
    }
}