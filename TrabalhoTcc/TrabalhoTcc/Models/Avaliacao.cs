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
        [MinLength(5, ErrorMessage="A avaliação deve conter no mínimo 5 caracteres")]
        public string AvaliacaoUsuario { get; set; }
        
        [Required]
        public byte NotaVoltarNovamente { get; set; }

        [Required]
        public byte NotaAgendamento { get; set; }

        [Required]
        public byte NotaExperienciaAtendimento { get; set; }

        [Required]
        public byte NotaCondicoesFisicasEstabelecimento { get; set; }

        [Required]
        public bool VoltariaNovamente { get; set; }
        
        [Required]
        public bool RecomendariaAlguem { get; set; }

        public bool AvaliacaoAprovadaParaIndex { get; set; }

        public virtual Agendamento Agendamento { get; set; }
        public int AgendamentoId { get; set; }

    }
}