using System;
using System.ComponentModel.DataAnnotations;

namespace SisHair.FuncionarioContext.Application.ViewModels
{
    public class FuncionarioViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome deve ser preenchido")]
        [MinLength(3, ErrorMessage = "Nome deve conter no mínimo 3 caracteres")]
        [MaxLength(100, ErrorMessage = "Nome deve conter no máximo 100 caracteres")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Data de nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "CPF deve ser preenchido")]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }
        
        public string Celular { get; set; }
        
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Email deve ser preenchido")]
        public string Email { get; set; }

        [Display(Name = "Cargo")]
        public CargoViewModel Cargo { get; set; }
        
        [Display(Name ="Ativo")]
        public bool RegistroFuncionarioAtivo { get; set; }
    }
}