using SisHair.CoreContext.BaseInterfaces.Commands;
using SisHair.FuncionarioContext.Application.Commands;

namespace SisHair.FuncionarioContext.Application.Handlers
{
    public interface IFuncionarioCommandHandler :
        ICommandHandler<CadastrarFuncionarioCommand>,
        ICommandHandler<AtualizarFuncionarioCommand>,
        ICommandHandler<RemoverFuncionarioCommand>
    {
    }
}
