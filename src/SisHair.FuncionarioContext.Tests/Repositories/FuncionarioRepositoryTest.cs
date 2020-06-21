using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SisHair.FuncionarioContext.Domain.Entities;
using SisHair.FuncionarioContext.Domain.ValueObjects;
using SisHair.FuncionarioContext.Infra.Data;
using System;

namespace SisHair.FuncionarioContext.Tests.Repositories
{
    [TestClass]
    public class FuncionarioRepositoryTest
    {
        private FuncionarioDataContext context;
        private FuncionarioRepository funcionarioRepository;

        [TestInitialize]
        public void Init()
        {
            SetupDatabase();
            funcionarioRepository = Substitute.ForPartsOf<FuncionarioRepository>(context);
        }

        [TestMethod]
        public void Add_FuncionarioWithoutCargo_SaveChanges_Return_False()
        {
            var funcionario = new Funcionario
            (
                "Teste Falha",
                DateTime.Today.AddYears(20),
                "76367366761",
                new Contato("47991573636", "4799234432", "teste@teste.com"),
                cargoId: 0,
                registroFuncionarioAtivo: true
            );

            funcionarioRepository.Add(funcionario);

            var result = funcionarioRepository.SaveChanges();

            Assert.IsFalse(result);
        } 
        
        [TestMethod]
        public void Add_FuncionarioWithCargo_SaveChanges_Return_True()
        {
            var cargo = CriarCargo();

            var funcionario = new Funcionario
            (
                "Teste Passa",
                DateTime.Today.AddYears(20),
                "76367366761",
                new Contato("47991573636", "4799234432", "teste@teste.com"),
                cargoId: cargo.Id,
                registroFuncionarioAtivo: true
            );

            funcionarioRepository.Add(funcionario);

            var result = funcionarioRepository.SaveChanges();

            Assert.IsTrue(result);
        }

        private Cargo CriarCargo()
        {
            var cargo = new Cargo("ADMINISTRADOR", "Cara manda em tudo nego", true);

            context.Cargos.Add(cargo);
            context.SaveChanges();

            return cargo;
        }

        private void SetupDatabase()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<FuncionarioDataContext>()
                .UseSqlServer
                (
                    @"Data Source=(localdb)\MSSQLLocalDB;" +
                    @"Initial Catalog=DbSisHairTest;" +
                    @"Integrated Security=True;" +
                    @"Connect Timeout=30;" +
                    @"Encrypt=False;" +
                    @"TrustServerCertificate=False;" +
                    @"ApplicationIntent=ReadWrite;" +
                    @"MultiSubnetFailover=False"
                ).UseInternalServiceProvider(serviceProvider);

            context = new FuncionarioDataContext(builder.Options);

            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }
    }
}
