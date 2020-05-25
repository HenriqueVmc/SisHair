using SisHair.CoreContext;
using System.ComponentModel.DataAnnotations;

namespace SisHair.FuncionarioContext.Domain.ValueObjects
{
    public class Contato : ValueObject
    {
        public Contato(string celular, string telefone, string email)
        {
            Celular = celular;
            Telefone = telefone;
            Email = email;
        }

        [MinLength(11, ErrorMessage = "Número de Celular inválido")]
        [MaxLength(15, ErrorMessage = "Número de Celular inválido")]
        [Display(Name = "Celular:")]
        public string Celular { get; private set; }

        [MinLength(11, ErrorMessage = "Número de Telefone inválido")]
        [MaxLength(15, ErrorMessage = "Número de Telefone inválido")]
        public string Telefone { get; private set; }

        [Display(Name = "Email:")]
        [EmailAddress]
        public string Email { get; private set; }
    }
}
