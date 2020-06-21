using MediatR;

namespace SisHair.CoreContext.BaseInterfaces
{
    public interface ICommand : IRequest<ICommandResult>
    {
        bool IsValid();
    }
}
