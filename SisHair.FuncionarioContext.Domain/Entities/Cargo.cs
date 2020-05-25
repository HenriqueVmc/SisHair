using SisHair.CoreContext;
using System.Collections.Generic;

namespace SisHair.FuncionarioContext.Domain.Entities
{
    public class Cargo : Entity
    {
        public Cargo(string nome, string descricao, bool registroCargoAtivo)
        {
            Nome = nome;
            Descricao = descricao;
            RegistroCargoAtivo = registroCargoAtivo;
        }

        public Cargo() { }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public bool RegistroCargoAtivo { get; set; }

        public ICollection<Funcionario> Funcionario { get; set; }
    }
}
