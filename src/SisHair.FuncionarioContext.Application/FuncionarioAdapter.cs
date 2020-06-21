using SisHair.FuncionarioContext.Application.Commands;
using SisHair.FuncionarioContext.Application.Events;
using SisHair.FuncionarioContext.Application.ViewModels;
using SisHair.FuncionarioContext.Domain.Entities;
using SisHair.FuncionarioContext.Domain.ValueObjects;
using System;

namespace SisHair.FuncionarioContext.Application
{
    public static class FuncionarioAdapter
    {
        public static FuncionarioViewModel FuncionarioToFuncionarioViewModel(Funcionario funcionario) =>
            funcionario == null ? null :
            new FuncionarioViewModel
            {
                Id = funcionario.Id,
                Nome = funcionario.Nome,
                DataNascimento = funcionario.DataNascimento,
                Cpf = funcionario.Cpf,
                Celular = funcionario.Contato.Celular,
                Telefone = funcionario.Contato.Telefone,
                Email = funcionario.Contato.Email,
                RegistroFuncionarioAtivo = true,
                Cargo = new CargoViewModel { Id = funcionario.CargoId, Nome = funcionario.Cargo?.Nome },
            };

        public static Funcionario CadastrarFuncionarioCommandToFuncionario(CadastrarFuncionarioCommand command) =>
            command == null ? null :
            new Funcionario(
                command.Nome,
                command.DataNascimento,
                command.Cpf,
                new Contato(command.Celular, command.Telefone, command.Email),
                command.CargoId,
                registroFuncionarioAtivo: true
            );

        public static Funcionario AtualizarFuncionarioCommandToFuncionario(AtualizarFuncionarioCommand command) =>
            command == null ? null :
            new Funcionario(
                command.Id,
                command.Nome,
                command.DataNascimento,
                command.Cpf,
                new Contato(command.Celular, command.Telefone, command.Email),
                command.CargoId,
                cargo: null,
                registroFuncionarioAtivo: true
            );

        public static CargoViewModel CargoToCargoViewModel(Cargo cargo) =>
            cargo == null ? null :
            new CargoViewModel
            {
                Id = cargo.Id,
                Nome = cargo.Nome
            };

        public static CadastrarFuncionarioEvent FuncionarioToCadastrarFuncionarioEvent(Funcionario funcionario) =>
            funcionario == null ? null :
            new CadastrarFuncionarioEvent(
                funcionario.Id,
                funcionario.Nome,
                funcionario.DataNascimento,
                funcionario.Cpf,
                funcionario.Contato.Celular,
                funcionario.Contato.Telefone,
                funcionario.Contato.Email,
                funcionario.CargoId
            );
    }
}
