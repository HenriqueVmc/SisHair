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

        [Required(ErrorMessage="O nome da Rua deve ser preenchida")]
        public string Rua { get; set; }
        
        [Required(ErrorMessage = "O Bairro deve ser preenchido")]
        public string Bairro { get; set; }
        
        [Required(ErrorMessage = "O Número deve ser preenchido")]
        public string NumeroDaCasa { get; set; }
        public string Complemento { get; set; }
        
        [Required(ErrorMessage = "O Estado deve ser preenchido")]
        public string Estado { get; set; }
        
        [Required(ErrorMessage = "A Cidade deve ser preenchida")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O CEP deve ser preenchido")]
        [MinLength(8, ErrorMessage="CEP inválido")]
        [MaxLength(8,ErrorMessage="CEP inválido")]
        public string CEP { get; set; }

        public virtual Funcionario Funcionario { get; set; }

        public int Id_Funcionario { get; set; }




    }
}