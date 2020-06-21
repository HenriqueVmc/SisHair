using System.ComponentModel.DataAnnotations;

namespace SisHair.FuncionarioContext.Application.ViewModels
{
    public class CargoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Cargo nao pode ser vazio")]
        [Display(Name = "Cargo:")]
        public string Nome { get; set; }
    }
}
