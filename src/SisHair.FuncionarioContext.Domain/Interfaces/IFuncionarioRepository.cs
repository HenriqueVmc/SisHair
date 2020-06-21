using SisHair.CoreContext.BaseInterfaces;
using SisHair.FuncionarioContext.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SisHair.FuncionarioContext.Domain.Interfaces
{
    public interface IFuncionarioRepository : IRepositoryBase<Funcionario>
    {
        Task<IEnumerable<Funcionario>> GetAllWithCargoAsNoTracking();
        Task<IEnumerable<Cargo>> GetAllCargosAsNoTracking();
        Task<Funcionario> GetByIdAsNoTracking(int id);
        bool ExistsBy(Func<Funcionario, bool> predicate);
    }
}
