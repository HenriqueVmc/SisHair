using Microsoft.EntityFrameworkCore;
using SisHair.FuncionarioContext.Domain.Entities;

namespace SisHair.FuncionarioContext.Infra.Data
{
    public class FuncionarioDataContext : DbContext
    {
        //Data Source=sis-hair-tcc.mssql.somee.com;Initial Catalog=sis-hair-tcc;Integrated Security=False;User ID=henriquevmc_SQLLogin_1;Password=tw7f23l1wn;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False        
        public FuncionarioDataContext(DbContextOptions<FuncionarioDataContext> options) : base(options) { }

        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Cargo> Cargos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new FuncionarioMapping());
            //modelBuilder.ApplyConfiguration(new CargoMapping());
            // OU
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FuncionarioDataContext).Assembly);
        }
    }
}