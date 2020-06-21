using SisHair.CoreContext;
using SisHair.FuncionarioContext.Domain.Entities;
using SisHair.FuncionarioContext.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SisHair.FuncionarioContext.Infra.Data
{
    public class FuncionarioService : ServiceBase<Funcionario, IFuncionarioRepository>, IFuncionarioService
    {
        private readonly IFuncionarioRepository repository;

        public FuncionarioService(IFuncionarioRepository funcionarioRepository) : base(funcionarioRepository)
        {
            this.repository = funcionarioRepository;
        }

        public async Task<IEnumerable<Funcionario>> BuscarFuncionariosComCargoAsync() =>
            await repository.GetAllWithCargoAsNoTracking();

        public async Task<IEnumerable<Cargo>> BuscarCargosDisponiveisAsync() =>
            await repository.GetAllCargosAsNoTracking();

        public async Task<Funcionario> BuscarPorIdAsync(int id) =>
            await repository.GetByIdAsNoTracking(id);

        public bool ExisteCPF(string cpf) =>
            repository.ExistsBy(e => e.Cpf == cpf);

        public bool ExisteEmail(string email) =>
            repository.ExistsBy(e => e.Contato.Email == email);

        private static void EnviarEmailFuncionario()
        {
            Console.WriteLine("Sending email");
        }
    }
}
