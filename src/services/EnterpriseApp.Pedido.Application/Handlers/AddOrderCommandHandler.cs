using EnterpriseApp.Core.DomainObjects;
using EnterpriseApp.Core.Extensions;
using EnterpriseApp.Core.Mediator;
using EnterpriseApp.Core.Messages;
using EnterpriseApp.Core.Messages.Integration;
using EnterpriseApp.MessageBus;
using EnterpriseApp.Pedido.Application.Commands;
using EnterpriseApp.Pedido.Application.DTO;
using EnterpriseApp.Pedido.Application.Events;
using EnterpriseApp.Pedido.Domain.Pedidos;
using EnterpriseApp.Pedido.Domain.Vouchers;
using EnterpriseApp.Pedido.Domain.Vouchers.Specifications;
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
        private readonly IMessageBus _messageBus;

        public AddOrderCommandHandler( 
            IMediatorHandler mediatorHandler,
            IOrderRepository orderRepository,
            IVoucherRepository voucherRepository,
            IMessageBus messageBus) : base(orderRepository, mediatorHandler)
        {
            _orderRepository = orderRepository;
            _voucherRepository = voucherRepository;
            _messageBus = messageBus;
        }

        public async Task<ValidationResult> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            // Validação do comando
            if (!request.Validate())
                return request.ValidationResult;

            // Mapear Pedido
            var order = MapOrder(request);

            // Aplicar voucher se houver
            if (!await ApplyVoucher(request, order)) 
                return request.ValidationResult;

            // Validar pedido
            if (!ValidateOrder(order, request.ValidationResult))
                return request.ValidationResult;

            // Processar pagamento
            if (!ProcessPayment(order, request).Result)
                return request.ValidationResult;

            // Se pagamento tudo ok!
            order.AuthorizeOrder();

            // Adicionar Evento
            await MediatorHandler.PublishEvent(new OrderRealizedEvent(order.Id, order.CustomerId));

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

        private async Task<bool> ApplyVoucher(AddOrderCommand request, Order order)
        {
            if (!request.HasUsedVoucher) 
                return true;

            var voucher = await _voucherRepository.GetVoucherByCode(request.Code);

            if (voucher == null)
            {
                request.ValidationResult.AddCustomError("Informed voucher does not exist.");
                return false;
            }

            var voucherValidation = new VoucherValidation().Validate(voucher);

            if (!voucherValidation.IsValid)
            {
                voucherValidation.Errors.ToList().ForEach(x => request.ValidationResult.AddCustomError(x.ErrorMessage));
                
                return false;
            }

            order.ApplyVoucher(voucher);

            voucher.DebitQuantity();

            _voucherRepository.Update(voucher);

            return true;
        }

        private static bool ValidateOrder(Order order, ValidationResult validationResult)
        {
            var originalTotalPrice = order.TotalPrice;
            var orderDiscount = order.Discount;

            order.CalculateOrderPrice();

            if (order.TotalPrice != originalTotalPrice || order.Discount != orderDiscount)
            {
                validationResult.AddCustomError("Total price order does not matches order calculated price.");
                return false;
            }

            return true;
        }

        public async Task<bool> ProcessPayment(Order order, AddOrderCommand request)
        {
            var orderInitializedEvent = new OrderInitializedIntegrationEvent
            {
                CustomerId = order.CustomerId,
                OrderID = order.Id,
                PaymentType = 0, // Fixed, only credit card
                Price = order.TotalPrice,
                CardName = request.CardName,
                CardNumber = request.CardNumber,
                CVV = request.CardCvv,
                MonthYearDueDate = request.CardExpirationDate
            };

            var response = await _messageBus.RequestAsync<OrderInitializedIntegrationEvent, ResponseMessage>(orderInitializedEvent);

            if (response.ValidationResult.IsValid)
                return true;

            response.ValidationResult.Errors.ForEach(x => request.ValidationResult.AddCustomError(x.ErrorMessage));

            return false;
        }
    }
}
