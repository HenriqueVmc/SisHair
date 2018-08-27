using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrabalhoTcc.Models
{
    public class Cargo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="Cargo nao pode ser vazio")]
        [Display(Name="Cargo:")]
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public virtual ICollection<Funcionario> Funcionario { get; set; }
    }
}