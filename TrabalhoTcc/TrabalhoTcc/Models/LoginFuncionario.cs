using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TrabalhoTcc.Repositorio;

namespace TrabalhoTcc.Models
{
    public class LoginFuncionario
    {


        public int Id { get; set; }

       // public Endereco Endereco { get; set; }
        public int Id_Endereco { get; set; }
        //public Cargo Cargo { get; set; }
        public int Id_Cargo { get; set; }

        [Required(ErrorMessage = "Nome não pode ser vasio")]
        [MinLength(7, ErrorMessage = "Nome não deve conter no mínimo 7 caracteres")]
        [MaxLength(100, ErrorMessage = "Nome não deve conter mias de 100 caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Dada de nascimento não pode ser vasio")]
        public DateTime DataNascimento { get; set; }
        [Required(ErrorMessage = "CPF não pode ser vasio")]
        public string CPF { get; set; }
        [Required(ErrorMessage = "CTPS não pode ser vasio")]
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Descricao { get; set; }
        public string Email { get; set; }
        public Cargo Cargo { get; internal set; }
        internal Endereco Endereco { get; set; }
    }
}