using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TrabalhoTcc.Models;
using TrabalhoTcc.Models.Conta;

namespace TrabalhoTcc.Context
{
    public class DBContext : DbContext
    {
        public DBContext() : base("Tcc")
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

    }
}