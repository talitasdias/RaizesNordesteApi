using RaizesNordeste.API.Application.DTOs.Pagamento;
using RaizesNordeste.API.Application.Interfaces;
using RaizesNordeste.API.Domain.Entities;
using RaizesNordeste.API.Domain.Enums;
using RaizesNordeste.API.Domain.Interfaces;

namespace RaizesNordeste.API.Application.Services
{
    public class PagamentoService : IPagamentoService
    {
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IPaymentGatewayService _gatewayService;

        public PagamentoService(
            IPagamentoRepository pagamentoRepository,
            IPedidoRepository pedidoRepository,
            IPaymentGatewayService gatewayService)
        {
            _pagamentoRepository = pagamentoRepository;
            _pedidoRepository = pedidoRepository;
            _gatewayService = gatewayService;
        }

        public async Task<PagamentoResponseDTO> ProcessarPagamentoAsync(PagamentoCreateDTO dto)
        {
            var pedido = await _pedidoRepository
                .GetDetailsByIdAsync(dto.PedidoId);

            if (pedido == null)
                throw new Exception("Pedido não encontrado.");

            if (pedido.StatusPedido != StatusPedido.AguardandoPagamento)
                throw new Exception("Pedido não pode ser pago.");

            var gatewayResponse = await _gatewayService
                .ProcessPaymentAsync(
                    pedido.ValorTotal,
                    dto.MetodoPagamento);

            var pagamentoAprovado = gatewayResponse.Sucesso;

            var pagamento = new Pagamento
            {
                PedidoId = pedido.Id,
                Valor = pedido.ValorTotal,
                Metodo = (MetodoPagamento)dto.MetodoPagamento,
                Status = pagamentoAprovado
                    ? StatusPagamento.Aprovado
                    : StatusPagamento.Recusado
            };

            await _pagamentoRepository.CreateAsync(pagamento);

            if (pagamentoAprovado)
            {
                pedido.StatusPedido = StatusPedido.Pago;

                if (pedido.Usuario.AceitouProgramaFidelidade)
                {
                    pedido.Usuario.PontosFidelidade += (int)pedido.ValorTotal;
                }

                await _pedidoRepository.UpdateAsync(pedido);
            }

            return new PagamentoResponseDTO
            {
                PagamentoId = pagamento.Id,
                PedidoId = pedido.Id,
                Valor = pagamento.Valor,
                StatusPagamento = pagamento.Status.ToString()
            };
        }
    }
}