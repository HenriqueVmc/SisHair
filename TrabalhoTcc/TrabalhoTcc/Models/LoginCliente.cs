using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrabalhoTcc.Models
{
    public class LoginCliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string DataNascimento { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string Telefone { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}