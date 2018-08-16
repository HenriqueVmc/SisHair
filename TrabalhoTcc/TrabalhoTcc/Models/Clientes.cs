using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrabalhoTcc.Models
{
    public class Clientes
    {
        public int Id { get; set; }
        [Required(ErrorMessage= "Nome não pode ser vazio")]
        [MinLength(7, ErrorMessage="Nome deve conter no mínimo 7 caracteres")]
        [MaxLength(100, ErrorMessage="Nome não deve exceder 100 caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage="Data de nascimento deve ser preenchida")]        
        public DateTime Data_nascimento { get; set; }
        [Required(ErrorMessage="Celular deve seer preenchida")]
        [MinLength(11,ErrorMessage="Digite um número de Celular valido")]
        [MaxLength(15, ErrorMessage="Digite um número de Celular valido")]
        public string Celular { get; set; }
        [Required(ErrorMessage="Telefone deve ser preenchido")]
        [MinLength(11,ErrorMessage="Digite um número de telefone valido")]
        [MaxLength(15,ErrorMessage="Digite um telefone de número valido")]
        public string Telefone { get; set; }
        //public string Email { get; set; }
    }
}