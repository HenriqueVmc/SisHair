using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrabalhoTcc.Models
{
    public class Avaliacao
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="Avaliação deve ser preenchida")]
        [MinLength(12, ErrorMessage="A avaliação deve conter no mínimo 12 caracteres")]
        public string AvaliacaoUsuario { get; set; }

        public virtual Agendamento Agendamento { get; set; }
        public int AgendamentoId { get; set; }

    }
}