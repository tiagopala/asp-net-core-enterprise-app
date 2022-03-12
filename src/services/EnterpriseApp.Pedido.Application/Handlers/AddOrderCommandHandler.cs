using EnterpriseApp.Core.DomainObjects;
using EnterpriseApp.Core.Extensions;
using EnterpriseApp.Core.Mediator;
using EnterpriseApp.Core.Messages;
using EnterpriseApp.Pedido.Application.Commands;
using EnterpriseApp.Pedido.Application.DTO;
using EnterpriseApp.Pedido.Domain.Pedidos;
using EnterpriseApp.Pedido.Domain.Vouchers;
using FluentValidation.Results;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EnterpriseApp.Pedido.Application.Handlers
{
    public class AddOrderCommandHandler : BaseHandler<Order>, IRequestHandler<AddOrderCommand, ValidationResult>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IVoucherRepository _voucherRepository;

        public AddOrderCommandHandler(
            IRepository<Order> repository, 
            IMediatorHandler mediatorHandler,
            IOrderRepository orderRepository,
            IVoucherRepository voucherRepository) : base(repository, mediatorHandler)
        {
            _orderRepository = orderRepository;
            _voucherRepository = voucherRepository;
        }

        public async Task<ValidationResult> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            // Validação do comando
            if (!request.Validate())
                return request.ValidationResult;

            // Mapear Pedido
            var order = MapOrder(request);

            // Aplicar voucher se houver
            if (!await AplicarVoucher(request, order)) 
                return request.ValidationResult;

            // Validar pedido
            if (!ValidarPedido(order))
                return request.ValidationResult;

            // Processar pagamento
            if (!ProcessarPagamento(order))
                return request.ValidationResult;

            // Se pagamento tudo ok!
            order.AuthorizeOrder();

            // Adicionar Evento
            //pedido.AddEvent(new PedidoRealizadoEvent(pedido.Id, pedido.CustomerId));

            // Adicionar Pedido Repositorio
            _orderRepository.Add(order);

            // Persistir dados de pedido e voucher
            var successfullOperation = await PersistData();

            if (successfullOperation is false)
            {
                request.ValidationResult.AddCustomError("The operation could not be completed. Try again later.");
                return request.ValidationResult;
            }

            return request.ValidationResult;
        }

        private Order MapOrder(AddOrderCommand request)
        {
            var address = new Address
            {
                Street = request.Address.Street,
                Number = request.Address.Number,
                Complement = request.Address.Complement,
                Neighbourhood = request.Address.Neighbourhood,
                Cep = request.Address.Cep,
                City = request.Address.City,
                State = request.Address.State
            };

            var order = new Order(request.CustomerId, request.TotalPrice, request.OrderItems.Select(x => x.ToOrderItem()).ToList(), request.HasUsedVoucher, request.Discount);

            order.ApplyAddress(address);

            return order;
        }

        private async Task<bool> AplicarVoucher(AddOrderCommand request, Order order)
        {
            if (!request.HasUsedVoucher) 
                return true;

            var voucher = await _voucherRepository.GetVoucherByCode(request.VoucherCode);

            if (voucher == null)
            {
                request.ValidationResult.AddCustomError("Informed voucher does not exist.");
                return false;
            }

            var voucherValidation = new VoucherValidation().Validate(voucher);

            if (!voucherValidation.IsValid)
            {
                voucherValidation.Errors.ToList().ForEach(m => AdicionarErro(m.ErrorMessage));
                return false;
            }

            order.AtribuirVoucher(voucher);
            voucher.DebitarQuantidade();

            _voucherRepository.Atualizar(voucher);

            return true;
        }

        private bool ValidarPedido(Pedido pedido)
        {
            var pedidoValorOriginal = pedido.ValorTotal;
            var pedidoDesconto = pedido.Desconto;

            pedido.CalcularValorPedido();

            if (pedido.ValorTotal != pedidoValorOriginal)
            {
                AdicionarErro("O valor total do pedido não confere com o cálculo do pedido");
                return false;
            }

            if (pedido.Desconto != pedidoDesconto)
            {
                AdicionarErro("O valor total não confere com o cálculo do pedido");
                return false;
            }

            return true;
        }

        public bool ProcessarPagamento(Pedido pedido)
        {
            return true;
        }
    }
}
