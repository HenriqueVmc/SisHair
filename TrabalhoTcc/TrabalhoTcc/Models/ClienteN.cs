using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace TrabalhoTcc.Models
{
    public class ClienteN
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome não pode ser vazio")]
        [MinLength(7, ErrorMessage = "Nome deve conter no mínimo 7 caracteres")]
        [MaxLength(100, ErrorMessage = "Nome não deve exceder 100 caracteres")]
        [Display(Name = "Nome:")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Data de nascimento deve ser preenchida")]
        [Display(Name = "Data de nascimento:")]
        [DataType(DataType.Date)]
        public DateTime Data_nascimento { get; set; }

        [Display(Name = "Celular: ")]
        public string Celular { get; set; }

        [Display(Name = ("Telefone: "))]
        public string Telefone { get; set; }

        [Display(Name = "Email: ")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="Senha é requirida")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage ="Por favor confirme a senha")]
        [DataType(DataType.Password)]
        public string ConfirmarSenha { get; set; }

    }
}