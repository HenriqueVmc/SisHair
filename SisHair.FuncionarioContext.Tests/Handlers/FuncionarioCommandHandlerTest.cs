using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NSubstitute;
using SisHair.FuncionarioContext.Application.Commands;
using SisHair.FuncionarioContext.Application.Handlers;
using SisHair.FuncionarioContext.Application.Events.Interfaces;
using SisHair.FuncionarioContext.Domain.Entities;
using SisHair.FuncionarioContext.Domain.Interfaces;
using System;

namespace SisHair.FuncionarioContext.Tests.Handlers
{
    [TestClass]
    public class FuncionarioCommandHandlerTest
    {
        private IFuncionarioService funcionarioService;
        private FuncionarioCommandHandler funcionarioCommandHandler;

        [TestInitialize]
        public void Init()
        {
            funcionarioService = Substitute.For<IFuncionarioService>();
            var cadastrarFuncionarioEventHandler = Substitute.For<ICadastrarFuncionarioEventHandler>();

            this.funcionarioCommandHandler = Substitute.ForPartsOf<FuncionarioCommandHandler>(funcionarioService, cadastrarFuncionarioEventHandler);
        }

        [TestMethod]
        [TestCategory("Handler")]
        public void Handle_CadastrarFuncionarioCommand_ExisteEmail_ReturnCommandResult_False()
        {
            // Arrange
            var command = new CadastrarFuncionarioCommand("TesteNome", DateTime.Today.AddYears(-20), "49383810877", "47991574566", string.Empty, "henrique@teste.com", 1);

            funcionarioService.ExisteEmail(Arg.Any<string>()).Returns(true);

            var expected = new CommandResult(false, "Não foi possível cadastrar Funcionário.")
                .AddNotifications("Email já cadastrado");

            var expectedNotifications = expected.GetNotifications();

            // Act
            var result = funcionarioCommandHandler.Handle(command);   
            var resultNotifications = ((CommandResult)result).GetNotifications();
            
            // Assert
            Assert.IsInstanceOfType(result, typeof(CommandResult));
            Assert.AreEqual(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(result));
            Assert.AreEqual(JsonConvert.SerializeObject(expectedNotifications), JsonConvert.SerializeObject(resultNotifications));
        }
        
        [TestMethod]
        [TestCategory("Handler")]
        public void Handle_CadastrarFuncionarioCommand_ExisteCPF_ReturnCommandResult_False()
        {
            // Arrange
            var command = new CadastrarFuncionarioCommand("TesteNome", DateTime.Today.AddYears(-20), "49383810877", "47991574566", string.Empty, "henrique@teste.com", 1);

            funcionarioService.ExisteEmail(Arg.Any<string>()).Returns(false);
            funcionarioService.ExisteCPF(Arg.Any<string>()).Returns(true);

            var expected = new CommandResult(false, "Não foi possível cadastrar Funcionário.")
                .AddNotifications("CPF já cadastrado");

            var expectedNotifications = expected.GetNotifications();

            // Act
            var result = (CommandResult)funcionarioCommandHandler.Handle(command);   
            var resultNotifications = result.GetNotifications();

            // Assert
            Assert.IsFalse(result.State);
            Assert.AreEqual(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(result));
            Assert.AreEqual(JsonConvert.SerializeObject(expectedNotifications), JsonConvert.SerializeObject(resultNotifications));
        }
        
        [TestMethod]
        [TestCategory("Handler")]
        public void Handle_CadastrarFuncionarioCommand_IsValidAndFailInsert_ReturnCommandResult_False()
        {
            // Arrange
            var command = new CadastrarFuncionarioCommand("TesteNome", DateTime.Today.AddYears(-20), "49383810877", "47991574566", string.Empty, "henrique@teste.com", 1);

            funcionarioService.ExisteEmail(Arg.Any<string>()).Returns(false);
            funcionarioService.ExisteCPF(Arg.Any<string>()).Returns(false);
            funcionarioService.Adicionar(Arg.Any<Funcionario>());
            funcionarioService.SaveChanges().Returns(false);

            var expected = new CommandResult(false, "Não foi possível cadastrar Funcionário.");

            // Act
            var result = (CommandResult)funcionarioCommandHandler.Handle(command);
            var resultNotifications = result.GetNotifications();

            // Assert
            Assert.IsFalse(result.State);
            Assert.AreEqual(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(result));
            Assert.IsTrue(resultNotifications.Count == 0);
        }
        
        [TestMethod]
        [TestCategory("Handler")]
        public void Handle_CadastrarFuncionarioCommand_IsValidAndSuccessInsert_ReturnCommandResult_True()
        {
            // Arrange
            var command = new CadastrarFuncionarioCommand("TesteNome", DateTime.Today.AddYears(-20), "49383810877", "47991574566", string.Empty, "henrique@teste.com", 1);

            funcionarioService.ExisteEmail(Arg.Any<string>()).Returns(false);
            funcionarioService.ExisteCPF(Arg.Any<string>()).Returns(false);
            funcionarioService.Adicionar(Arg.Any<Funcionario>());
            funcionarioService.SaveChanges().Returns(true);

            var expected = new CommandResult(true, "Funcionário cadastrado com sucesso!");

            // Act
            var result = (CommandResult)funcionarioCommandHandler.Handle(command);

            // Assert
            Assert.IsTrue(result.State);
            Assert.AreEqual(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(result));
        }        
    }
}
