using SisHair.CoreContext.BaseInterfaces.Commands;
using System;

namespace SisHair.FuncionarioContext.Application.Events.Interfaces
{
    public delegate void CommandEventHandler<T>(T command) where T : ICommand;

    public interface IFuncionarioEventHandler<T> where T : ICommand
    {
        void InvokeEvents(T command);
        void SubscribeEvents(Action<T> action);
    }
}
