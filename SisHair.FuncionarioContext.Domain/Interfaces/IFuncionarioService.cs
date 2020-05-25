using SisHair.CoreContext.BaseInterfaces;
using SisHair.FuncionarioContext.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SisHair.FuncionarioContext.Domain.Interfaces
{
    public interface IFuncionarioService : IServiceBase<Funcionario>
    {
        Task<IEnumerable<Funcionario>> BuscarFuncionariosComCargoAsync();
        Task<IEnumerable<Cargo>> BuscarCargosDisponiveisAsync();
        Task<Funcionario> BuscarPorIdAsync(int id);
        bool ExisteEmail(string email);
        bool ExisteCPF(string cpf);
    }
}
