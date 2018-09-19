using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrabalhoTcc.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage= "Nome não pode ser vazio")]
        [MinLength(7, ErrorMessage="Nome deve conter no mínimo 7 caracteres")]
        [MaxLength(100, ErrorMessage="Nome não deve exceder 100 caracteres")]
        [Display(Name="Nome:")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage="Data de nascimento deve ser preenchida")]       
        [Display(Name="Data de nascimento:")]
        [DataType(DataType.Date)]
        public DateTime Data_nascimento { get; set; }

        [Required(ErrorMessage="Celular deve ser preenchida")]
        [MinLength(11,ErrorMessage="Digite um número de Celular valido")]
        [MaxLength(15, ErrorMessage="Digite um número de Celular valido")]
        [Display(Name="Celular: ")]
        public string Celular { get; set; }

        [Required(ErrorMessage="Telefone deve ser preenchido")]
        [MinLength(11,ErrorMessage="Digite um número de telefone valido")]
        [MaxLength(15,ErrorMessage="Digite um telefone de número valido")]
        [Display(Name=("Telefone: "))]
        public string Telefone { get; set; }


        [Display(Name="Email: ")]
        [EmailAddress]
        [Required(ErrorMessage = "Email deve ser preenchido")]
        public string Email { get; set; }              
    }
}