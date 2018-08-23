using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrabalhoTcc.Models
{
    public class Endereco
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage="A rua não pode ser vasia")]
        public string Rua { get; set; }
        [Required(ErrorMessage = "O bairro não pode ser vasio")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "O número da casa não pode ser vasio")]
        public string NumeroDaCasa { get; set; }
        public string Complemento { get; set; }
        [Required(ErrorMessage = "O estado não pode ser vasio")]
        public string Estado { get; set; }
        [Required(ErrorMessage = "A cidada não pode ser vasia")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "O CEP não pode ser vasio")]
        [MinLength(8, ErrorMessage="CEP inválido")]
        [MaxLength(8,ErrorMessage="CEP inválido")]
        public string CEP { get; set; }

        public virtual Funcionario Funcionario { get; set; }

        public int Id_Funcionario { get; set; }




    }
}