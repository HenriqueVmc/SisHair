using SisHair.CoreContext;
using SisHair.FuncionarioContext.Domain.ValueObjects;
using System;

namespace SisHair.FuncionarioContext.Domain.Entities
{
    public class Funcionario : Entity
    {
        public string Nome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string Cpf { get; private set; }
        public Cargo Cargo { get; private set; }
        public int CargoId { get; private set; }
        public Contato Contato { get; private set; }
        public bool RegistroFuncionarioAtivo { get; private set; }

        public Funcionario() { }

        public Funcionario
            (
                string nome,
                DateTime dataNascimento,
                string cpf,
                Contato contato,
                int cargoId,
                bool registroFuncionarioAtivo
            )
        {
            Nome = nome;
            DataNascimento = dataNascimento;
            Cpf = cpf;
            Contato = contato;
            CargoId = cargoId;
            RegistroFuncionarioAtivo = registroFuncionarioAtivo;
        }

        public Funcionario
            (
                int id,
                string nome,
                DateTime dataNascimento,
                string cpf,
                Contato contato,
                int cargoId,
                Cargo cargo,
                bool registroFuncionarioAtivo
            )
        {
            Id = id;
            Nome = nome;
            DataNascimento = dataNascimento;
            Cpf = cpf;
            Contato = contato;
            CargoId = cargoId;
            Cargo = cargo;
            RegistroFuncionarioAtivo = registroFuncionarioAtivo;
        }
    }
}
