using MediatR;
using SisHair.CoreContext.BaseInterfaces;
using SisHair.FuncionarioContext.Application.Commands;
using SisHair.FuncionarioContext.Application.Events;
using SisHair.FuncionarioContext.Domain.Entities;
using SisHair.FuncionarioContext.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SisHair.FuncionarioContext.Application.Handlers
{
    public class IFuncionarioCommandHandler :
        IRequestHandler<CadastrarFuncionarioCommand, ICommandResult>,
        IRequestHandler<AtualizarFuncionarioCommand, ICommandResult>,
        IRequestHandler<RemoverFuncionarioCommand, ICommandResult>
    {
        private readonly IFuncionarioService service;
        private readonly INotificationHandler<CadastrarFuncionarioEvent> cadastrarFuncionarioEventHandler;

        public IFuncionarioCommandHandler(IFuncionarioService service,
            INotificationHandler<CadastrarFuncionarioEvent> cadastrarFuncionarioEventHandler)
        {
            this.service = service;
            this.cadastrarFuncionarioEventHandler = cadastrarFuncionarioEventHandler;
        }

        public async Task<ICommandResult> Handle(CadastrarFuncionarioCommand command, CancellationToken cancellationToken)
        {
            var result = new CommandResult(false, "Não foi possível cadastrar Funcionário.");

            if (!command.IsValid())
                return result.AddNotifications("Dados Inválidos!");

            if (service.ExisteEmail(command.Email))
                return result.AddNotifications("Email já cadastrado");

            if (service.ExisteCPF(command.Cpf))
                return result.AddNotifications("CPF já cadastrado");

            var funcionario = new Funcionario();

            try
            {
                funcionario = FuncionarioAdapter.CadastrarFuncionarioCommandToFuncionario(command);
                service.Adicionar(funcionario);

                if (!service.SaveChanges()) return result;
            }
            catch (Exception ex)
            {
                return new CommandResult(false, $"Ocorreu um erro ao cadastrar Funcionário. Erro.: {ex.Message}");
            }

            new Thread(() => cadastrarFuncionarioEventHandler.Handle(
                FuncionarioAdapter.FuncionarioToCadastrarFuncionarioEvent(funcionario), CancellationToken.None)
            ).Start();

            return new CommandResult(true, "Funcionário cadastrado com sucesso!");
        }

        public async Task<ICommandResult> Handle(AtualizarFuncionarioCommand command, CancellationToken cancellationToken)
        {
            var result = new CommandResult(true, "Não foi possível atualizar Funcionário!");

            // Ações necessárias para resolver o comando
            if (!command.IsValid())
                return result.AddNotifications("Dados Inválidos");

            if (service.ExisteEmail(command.Email))
                return result.AddNotifications("Email já cadastrado");

            if (service.ExisteCPF(command.Cpf))
                return result.AddNotifications("CPF já cadastrado");

            try
            {
                var funcionarioAtualizado = FuncionarioAdapter.AtualizarFuncionarioCommandToFuncionario(command);
                //var funcionarioAtual = service.BuscarPorId(funcionarioAtualizado.Id);
                //funcionarioAtual = funcionarioAtualizado;

                service.Atualizar(funcionarioAtualizado);

                if (!service.SaveChanges()) return result;
            }
            catch (Exception ex)
            {
                return new CommandResult(false, $"Ocorreu um erro ao atualizar Funcionário. Erro.: {ex.Message}");
            }

            // Invocar eventos

            return new CommandResult(true, "Funcionário atualizado com sucesso!");
        }

        public async Task<ICommandResult> Handle(RemoverFuncionarioCommand command, CancellationToken cancellationToken)
        {
            var result = new CommandResult(true, "Não foi possível atualizar Funcionário!");

            if (!command.IsValid())
                return result.AddNotifications("Dados Inválidos");

            try
            {
                var funcionario = service.BuscarPorId(command.Id);

                if (funcionario == null)
                    return result.AddNotifications("Funcionário não cadastrado");

                service.Remover(funcionario);

                if (!service.SaveChanges()) return result;
            }
            catch (Exception ex)
            {
                return new CommandResult(false, $"Ocorreu um erro ao remover Funcionário. Erro.: {ex.Message}");
            }

            // Invocar eventos

            return new CommandResult(true, "Funcionário removido com sucesso!");
        }
    }
}
