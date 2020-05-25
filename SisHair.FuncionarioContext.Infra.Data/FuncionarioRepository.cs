using Microsoft.EntityFrameworkCore;
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

        public void Dispose() => context.Dispose();

        public virtual bool SaveChanges()
        {
            var success = false;

            try
            {
                success = context.SaveChanges() >= 0;
            }
            catch(Exception ex)
            {
                // Logger
            }

            return success;
        }

        public virtual void Add(Funcionario funcionario) => 
            context.Funcionarios.Add(funcionario);

        public virtual void Update(Funcionario funcionario) => 
            context.Funcionarios.Update(funcionario);

        public virtual void Remove(Funcionario funcionario) => 
            context.Remove(funcionario);

        public virtual Funcionario GetById(int id) => 
            context.Funcionarios.Find(id);

        public virtual IQueryable<Funcionario> GetAll()
        {
            var teste = context.Funcionarios.AsQueryable();
            return teste;
        }

        public virtual async Task<IEnumerable<Funcionario>> GetAllWithCargoAsNoTracking() => 
            await context.Funcionarios.Include(i => i.Cargo).AsNoTracking().ToListAsync();

        public async Task<IEnumerable<Cargo>> GetAllCargosAsNoTracking() =>
            await context.Cargos.AsNoTracking().ToListAsync();

        public async Task<Funcionario> GetByIdAsNoTracking(int id) =>
            await context.Funcionarios.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);

        public bool ExistsBy(Func<Funcionario, bool> predicate) =>
            context.Funcionarios.Any(predicate);
    }
}
