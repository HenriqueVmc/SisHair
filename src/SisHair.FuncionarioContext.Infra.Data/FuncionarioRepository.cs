using Microsoft.EntityFrameworkCore;
using SisHair.CoreContext.BaseInterfaces;
using SisHair.FuncionarioContext.Domain.Entities;
using SisHair.FuncionarioContext.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SisHair.FuncionarioContext.Infra.Data
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly FuncionarioDataContext context;

        public FuncionarioRepository(FuncionarioDataContext context)
        {
            this.context = context;
        }

        public IUnitOfWork UnitOfWork => context;

        public void Dispose() => context.Dispose();

        public virtual void Add(Funcionario obj) => context.Funcionarios.Add(obj);

        public virtual void Update(Funcionario obj) => context.Funcionarios.Update(obj);

        public virtual void Remove(Funcionario obj) => context.Remove(obj);

        public virtual Funcionario GetById(int id) => context.Funcionarios.Find(id);

        public virtual IQueryable<Funcionario> GetAll() => context.Funcionarios.AsNoTracking();

        public virtual async Task<IEnumerable<Funcionario>> GetAllWithCargoAsNoTracking() => 
            await context.Funcionarios.Include(i => i.Cargo).AsNoTracking().ToListAsync();

        public async Task<IEnumerable<Cargo>> GetAllCargosAsNoTracking() =>
            await context.Cargos.AsNoTracking().ToListAsync();

        public async Task<Funcionario> GetByIdAsNoTracking(int id) =>
            await context.Funcionarios.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);

        public bool ExistsBy(Func<Funcionario, bool> predicate) => context.Funcionarios.Any(predicate);
    }
}
