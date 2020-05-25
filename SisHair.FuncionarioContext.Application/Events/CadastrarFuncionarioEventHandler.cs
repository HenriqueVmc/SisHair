using SisHair.FuncionarioContext.Application.Commands;
using SisHair.FuncionarioContext.Application.Events.Interfaces;
using System;

namespace SisHair.FuncionarioContext.Application.Events
{
    public class CadastrarFuncionarioEventHandler : ICadastrarFuncionarioEventHandler
    {
        private event CommandEventHandler<CadastrarFuncionarioCommand> Events;

        public CadastrarFuncionarioEventHandler()
        {
            SubscribeEvents(OnSendEmail);
            SubscribeEvents(OnLogRegister);
        }

        public void OnSendEmail(CadastrarFuncionarioCommand sender)
        {
            Console.WriteLine($"--- Sending Email---\n" +
                $"\tObj: {sender}\n" +
                "\tEvent: {e}");
        }

        public void OnLogRegister(CadastrarFuncionarioCommand sender)
        {
            Console.WriteLine($"--- Log Register ---\n" +
                $"\tObj: {sender}\n" +
                "\tEvent: {e}");
        }        

        public void InvokeEvents(CadastrarFuncionarioCommand command)
        {
            Events?.Invoke(command);
        }

        public void SubscribeEvents(Action<CadastrarFuncionarioCommand> action)
        {
            Events += action.Invoke;
        }
    }
}
