using System.Data.Entity;
using SisHair.Presentation.Web.MVC.Old.Models;
using SisHair.Presentation.Web.MVC.Old.Models.Conta;

namespace SisHair.Presentation.Web.MVC.Old.Context
{
    public class DBContext : DbContext
    {
        public DBContext() : base("Tcc")//Data Source=sis-hair-tcc.mssql.somee.com;Initial Catalog=sis-hair-tcc;Integrated Security=False;User ID=henriquevmc_SQLLogin_1;Password=tw7f23l1wn;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<LoginFuncionario> LoginFuncionarios { get; set; }
        public DbSet<EnderecoFuncionario> EnderecoFuncionarios { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<LoginCliente> LoginClientes { get; set; }
        public DbSet<Solicitacao> Solicitacoes { get; set; }
        public DbSet<CodigoCliente> CodigosClientes { get; set; }
        public DbSet<Caixa> Caixa { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<Permissoes> permissoes { get; set; }
        public DbSet<ServicosAgendamento> ServicosAgendamento { get; set; }
        public DbSet<ServicosSolicitacao> ServicosSolicitacao { get; set; }

        public DbSet<segurancaLogin> segLogin { get; set; }

    }
}