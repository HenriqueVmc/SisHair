
using SisHair.CoreContext.BaseInterfaces;
using System;

namespace SisHair.FuncionarioContext.Application.Commands
{
    public class AtualizarFuncionarioCommand : ICommand
    {
        public AtualizarFuncionarioCommand(
            int id,
            string nome,
            DateTime dataNascimento,
            string cpf,
            string celular,
            string telefone,
            string email,
            int cargoId
            )
        {
            this.Id = id;
            this.Nome = nome;
            this.DataNascimento = dataNascimento;
            this.Cpf = cpf;
            this.Celular = celular;
            this.Telefone = telefone;
            this.Email = email;
            this.CargoId = cargoId;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string Cpf { get; private set; }
        public string Celular { get; private set; }
        public string Telefone { get; private set; }
        public string Email { get; private set; }
        public int CargoId { get; private set; }

        public bool IsValid()
        {
            return false;
        }
    }
}
