using SisHair.CoreContext.BaseInterfaces;
using System.Threading.Tasks;

namespace SisHair.CoreContext.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T anEvent) where T : IEvent;
        Task<ICommandResult> SendCommand<T>(T command) where T : ICommand;
    }
}
