using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrabalhoTcc.Models
{
    public class Funcionario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome não pode ser vasio")]
        [MinLength(7, ErrorMessage = "Nome não deve conter no mínimo 7 caracteres")]
        [MaxLength(100, ErrorMessage = "Nome não deve conter mias de 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Dada de nascimento não pode ser vasio")]
        public DateTime DataNascimento { get; set; }
       
        [Required(ErrorMessage = "CPF não pode ser vasio")]
        public string Cpf { get; set; }

        [MinLength(11, ErrorMessage = "Digite um número de telefone valido")]
        [MaxLength(15, ErrorMessage = "Digite um telefone de número valido")]
        [Display(Name = ("Telefone: "))]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Celular deve seer preenchida")]
        [MinLength(11, ErrorMessage = "Digite um número de Celular valido")]
        [MaxLength(15, ErrorMessage = "Digite um número de Celular valido")]
        [Display(Name = "Celular: ")]
        public string Celular { get; set; }

        [Display(Name = "Email: ")]
        [EmailAddress]
        public string Email { get; set; }

        public Cargo Cargo { get; set; }
        public int Id_Cargo { get; set; }


    }
}