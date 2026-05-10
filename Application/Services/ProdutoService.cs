using RaizesNordeste.API.Application.DTOs;
using RaizesNordeste.API.Application.Interfaces;
using RaizesNordeste.API.Domain.Interfaces;

namespace RaizesNordeste.API.Application.Services
{
    public class ProdutoService(IProdutoRepository repository) : IProdutoService
    {
        private readonly IProdutoRepository _repository = repository;

        public async Task<ProdutoResponseDTO?> UpdateAsync(ProdutoUpdateDTO produtoDto, int id)
        {
            var produtoAtualizado = await _repository.UpdateAsync(produtoDto, id);
            if (produtoAtualizado is null)
                return null;
            
            return new ProdutoResponseDTO
            {
                Id = produtoAtualizado.Id,
                Nome = produtoAtualizado.Nome,
                Descricao = produtoAtualizado.Descricao,
                Preco = produtoAtualizado.Preco,
                Disponivel = produtoAtualizado.Disponivel
            };
        }

        public async Task<ProdutoCreateResponseDTO> CreateAsync(ProdutoCreateDTO produtoDto)
        {
            var produtoCriado = await _repository.CreateAsync(produtoDto);

            return new ProdutoCreateResponseDTO
            {
                Id = produtoCriado.Id,
                Nome = produtoCriado.Nome,
                Descricao = produtoCriado.Descricao,
                Disponivel = produtoCriado.Disponivel,
                Preco = produtoCriado.Preco,
                DataCriacao = produtoCriado.DataCriacao
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<ProdutoResponseDTO?> GetByIdAsync(int id)
        {
            var produto = await _repository.GetByIdAsync(id);
            
            if (produto is null)
                return null;

            return new ProdutoResponseDTO
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                Disponivel = produto.Disponivel
            };
        }

        public async Task<PaginacaoResponseDTO<ProdutoResponseDTO>> GetAllAsync(int pagina, int tamanhoPagina)
        {
            var (produtos, total) = await _repository.GetAllAsync(pagina, tamanhoPagina);

            var itens = produtos.Select(p => new ProdutoResponseDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                Descricao = p.Descricao,
                Preco = p.Preco,
                Disponivel = p.Disponivel
            }).ToList();

            return new PaginacaoResponseDTO<ProdutoResponseDTO>
            {
                Pagina = pagina,
                TamanhoPagina = tamanhoPagina,
                TotalItens = total,
                TotalPaginas = (int)Math.Ceiling((double)total / tamanhoPagina),
                Itens = itens
            };
        }
    }
}