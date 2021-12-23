using EnterpriseApp.Core.Messages;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace EnterpriseApp.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T evnt) where T : Event;
        Task<ValidationResult> SendCommand<T>(T command) where T : Command;
    }
}
