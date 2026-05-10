namespace RaizesNordeste.API.Application.DTOs
{
    public class PaginacaoResponseDTO<T>
    {
        public int Pagina { get; set; }
        public int TamanhoPagina { get; set; }
        public int TotalItens { get; set; }
        public int TotalPaginas { get; set; }
        public IEnumerable<T> Itens { get; set; } = [];
    }
}