using SisHair.FuncionarioContext.Application.ViewModels;
using SisHair.FuncionarioContext.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SisHair.FuncionarioContext.Application.Queries
{
    public class FuncionarioQueries : IFuncionarioQueries
    {
        private readonly IFuncionarioService funcionarioService;

        public FuncionarioQueries(IFuncionarioService funcionarioService)
        {
            this.funcionarioService = funcionarioService;
        }

        public async Task<IEnumerable<FuncionarioViewModel>> BuscarFuncionariosComCargoAsync()
        {
            var funcionariosComCargo = await funcionarioService.BuscarFuncionariosComCargoAsync();
            return funcionariosComCargo.Select(FuncionarioAdapter.FuncionarioToFuncionarioViewModel);
        }

        public async Task<IEnumerable<CargoViewModel>> BuscarCargosDisponiveisAsync()
        {
            var cargosDisponiveis = await funcionarioService.BuscarCargosDisponiveisAsync();
            return cargosDisponiveis.Select(FuncionarioAdapter.CargoToCargoViewModel);
        }

        public async Task<FuncionarioViewModel> BuscarPorIdAsync(int id)
        { 
            var funcionario = await funcionarioService.BuscarPorIdAsync(id);
            return FuncionarioAdapter.FuncionarioToFuncionarioViewModel(funcionario);
        }
    }
}
