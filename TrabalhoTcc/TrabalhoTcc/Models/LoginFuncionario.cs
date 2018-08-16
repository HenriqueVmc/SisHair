using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrabalhoTcc.Models
{
    public class LoginFuncionario
    {


        public int Id { get; set; }
        [Required(ErrorMessage = "Nome não pode ser vasio")]
        [MinLength(7, ErrorMessage = "Nome não deve conter no mínimo 7 caracteres")]
        [MaxLength(100, ErrorMessage = "Nome não deve conte r mias de 100 caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Dada de nascimento não pode ser vasio")]
        public DateTime DataNascimento { get; set; }
        [Required(ErrorMessage = "CPF não pode ser vasio")]
        public string CPF { get; set; }
        [Required(ErrorMessage = "CTPS não pode ser vasio")]
        public string CTPS { get; set; }
        public string Cargo { get; set; }
        public string Contato { get; set; }
        public string Endereco { get; set; }

    }
}