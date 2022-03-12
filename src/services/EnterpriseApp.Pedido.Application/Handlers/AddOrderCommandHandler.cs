using EnterpriseApp.Core.DomainObjects;
using EnterpriseApp.Core.Mediator;
using EnterpriseApp.Core.Messages;
using EnterpriseApp.Pedido.Application.Commands;
using EnterpriseApp.Pedido.Domain.Pedidos;
using FluentValidation.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EnterpriseApp.Pedido.Application.Handlers
{
    public class AddOrderCommandHandler : BaseHandler<Order>, IRequestHandler<AddOrderCommand, ValidationResult>
    {
        public AddOrderCommandHandler(IRepository<Order> repository, IMediatorHandler mediatorHandler) : base(repository, mediatorHandler) { }

        public Task<ValidationResult> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
