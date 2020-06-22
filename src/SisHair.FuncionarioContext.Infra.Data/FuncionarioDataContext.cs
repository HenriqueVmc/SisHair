using Microsoft.EntityFrameworkCore;
using SisHair.CoreContext.BaseInterfaces;
using SisHair.FuncionarioContext.Domain.Entities;
using System.Threading.Tasks;

namespace SisHair.FuncionarioContext.Infra.Data
{
    public class FuncionarioDataContext : DbContext, IUnitOfWork
    {        
        public FuncionarioDataContext(DbContextOptions<FuncionarioDataContext> options) : base(options) { }

        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Cargo> Cargos { get; set; }

        public async Task<bool> Commit() => await base.SaveChangesAsync() > 0;

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FuncionarioDataContext).Assembly);
    }
}