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


        [Required(ErrorMessage="Cargo nao pode ser vasio")]
        [MaxLength(ErrorMessage="O cargo nao deve conter mais de 50 caracteres")]
        public string Nome { get; set; }

        [MaxLength(ErrorMessage="A descrição nao deve conter mais de 100 caracteres")] 
        public string Descricao { get; set; }

        public virtual ICollection<Funcionario> Funcionario { get; set; }
    }
}