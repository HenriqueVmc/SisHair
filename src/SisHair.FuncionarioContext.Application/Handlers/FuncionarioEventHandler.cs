using MediatR;
using SisHair.FuncionarioContext.Application.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SisHair.FuncionarioContext.Application.Handlers
{
    public class FuncionarioEventHandler :
        INotificationHandler<CadastrarFuncionarioEvent>
    {
        public class CadastrarFuncionarioEventArgs : EventArgs
        {
            public CadastrarFuncionarioEventArgs(CadastrarFuncionarioEvent anEvent)
            {
                Event = anEvent;
            }

            public CadastrarFuncionarioEvent Event { get; }
        }

        private event EventHandler<CadastrarFuncionarioEventArgs> CadastrarFuncionarioEvents;

        public async Task Handle(CadastrarFuncionarioEvent anEvent, CancellationToken cancellationToken)
        {
            // Assinando Eventos ao delegate
            CadastrarFuncionarioEvents += OnSendEmail;
            CadastrarFuncionarioEvents += OnLogRegister;            

            new Thread(() => CadastrarFuncionarioEvents.Invoke(this, new CadastrarFuncionarioEventArgs(anEvent))).Start();
        }
      
        private void OnSendEmail(object obj, CadastrarFuncionarioEventArgs args)
        {
            Console.WriteLine($"--- Sending Email---\n" +
                $"\tObj: {args.Event}\n");
            
            Thread.Sleep(2000);
        }

        private void OnLogRegister(object obj, CadastrarFuncionarioEventArgs args)
        {
            Console.WriteLine($"--- Log Register ---\n" +
                $"\tObj: {args.Event}\n");

            Thread.Sleep(2000);
        }
    }
}
