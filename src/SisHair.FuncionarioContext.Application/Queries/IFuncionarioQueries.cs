using SisHair.FuncionarioContext.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SisHair.FuncionarioContext.Application.Queries
{
    public interface IFuncionarioQueries
    {
        Task<IEnumerable<FuncionarioViewModel>> BuscarFuncionariosComCargoAsync();
        Task<IEnumerable<CargoViewModel>> BuscarCargosDisponiveisAsync();
        Task<FuncionarioViewModel> BuscarPorIdAsync(int id);
    }
}
