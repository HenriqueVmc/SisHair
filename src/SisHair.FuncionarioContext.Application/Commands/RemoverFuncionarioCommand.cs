using SisHair.CoreContext.BaseInterfaces;

namespace SisHair.FuncionarioContext.Application.Commands
{
    public class RemoverFuncionarioCommand : ICommand
    {
        public int Id { get; set; }

        public RemoverFuncionarioCommand(int id)
        {
            Id = id;
        }

        public bool IsValid()
        {
            return true;
        }
    }
}
