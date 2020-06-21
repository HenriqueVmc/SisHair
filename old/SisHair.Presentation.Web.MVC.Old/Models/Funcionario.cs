using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SisHair.Presentation.Web.MVC.Old.Models
{
    public class Funcionario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome deve ser preenchido")]
        [MinLength(3, ErrorMessage = "Nome deve conter no mínimo 3 caracteres")]
        [MaxLength(100, ErrorMessage = "Nome deve conter no máximo 100 caracteres")]
        [Display(Name = "Nome:")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Dada de nascimento deve ser preenchida")]
        [Display(Name = "Data de Nascimento:")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "CPF deve ser preenchido")]
        [Display(Name = "CPF:")]
        public string Cpf { get; set; }

        [MinLength(11, ErrorMessage = "Número de Celular inválido")]
        [MaxLength(15, ErrorMessage = "Número de Celular inválido")]
        [Display(Name = "Celular:")]
        public string Celular { get; set; }

        [MinLength(11, ErrorMessage = "Número de Telefone inválido")]
        [MaxLength(15, ErrorMessage = "Número de Telefone inválido")]
        public string Telefone { get; set; }

        [Display(Name = "Email:")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Cargo:")]
        public int CargoId { get; set; }

        [Display(Name = "Cargo:")]
        public virtual Cargo Cargo { get; set; }
        [DefaultValue(true)]
        public bool RegistroFuncionarioAtivo { get; set; }

    }
}