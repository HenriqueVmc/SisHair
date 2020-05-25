using SisHair.CoreContext.BaseInterfaces.Commands;
using SisHair.FuncionarioContext.Application.Commands;
using SisHair.FuncionarioContext.Application.Events.Interfaces;
using SisHair.FuncionarioContext.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace SisHair.FuncionarioContext.Application.Handlers
{
    public class FuncionarioCommandHandler : IFuncionarioCommandHandler
    {
        private readonly IFuncionarioService service;
        private readonly ICadastrarFuncionarioEventHandler cadastrarFuncionarioEventHandler;

        public FuncionarioCommandHandler(IFuncionarioService service,
            ICadastrarFuncionarioEventHandler cadastrarFuncionarioEventHandler)
        {
            this.service = service;
            this.cadastrarFuncionarioEventHandler = cadastrarFuncionarioEventHandler;
        }

        public ICommandResult Handle(CadastrarFuncionarioCommand command)
        {
            var result = new CommandResult(false, "Não foi possível cadastrar Funcionário.");
            
            if (!command.IsValid())
                return result.AddNotifications("Dados Inválidos!");
            
            if(service.ExisteEmail(command.Email))
                return result.AddNotifications("Email já cadastrado");

            if (service.ExisteCPF(command.Cpf))
                return result.AddNotifications("CPF já cadastrado");

            try
            {
                var funcionario = FuncionarioAdapter.CadastrarFuncionarioCommandToFuncionario(command);
                service.Adicionar(funcionario);

                if (!service.SaveChanges()) return result;
            }
            catch(Exception ex)
            {
                return new CommandResult(false, $"Ocorreu um erro ao cadastrar Funcionário. Erro.: {ex.Message}");
            }

            Task.Run(() => cadastrarFuncionarioEventHandler.InvokeEvents(command));

            return new CommandResult(true, "Funcionário cadastrado com sucesso!");
        }
        
        public ICommandResult Handle(AtualizarFuncionarioCommand command)
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

        public ICommandResult Handle(RemoverFuncionarioCommand command)
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
