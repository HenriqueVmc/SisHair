using MediatR;
using SisHair.CoreContext.BaseInterfaces;
using System.Threading.Tasks;

namespace SisHair.CoreContext.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator mediator;

        public MediatorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        public async Task PublishEvent<T>(T anEvent) where T : IEvent =>
            await mediator.Publish(anEvent);

        public async Task<ICommandResult> SendCommand<T>(T command) where T : ICommand =>
            await mediator.Send(command);
    }
}
