using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrabalhoTcc.Models
{
    public class LoginFuncionario
    {
   

        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
        public string CTPS { get; set; }
        public string Cargo { get; set; }
        public string Contato { get; set; }
        public string Endereco { get; set; }

    }
}