using SisHair.FuncionarioContext.Application.Commands;

namespace SisHair.FuncionarioContext.Application.Events.Interfaces
{
    public interface ICadastrarFuncionarioEventHandler : IFuncionarioEventHandler<CadastrarFuncionarioCommand>
    {
        void OnSendEmail(CadastrarFuncionarioCommand command);
        void OnLogRegister(CadastrarFuncionarioCommand command);
    }
}