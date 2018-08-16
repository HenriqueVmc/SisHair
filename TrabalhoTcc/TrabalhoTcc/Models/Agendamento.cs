using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrabalhoTcc.Models
{
    public class Agendamento
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Hora deve ser preenchida")]
        public double Hora_inicio { get; set; }
        [Required(ErrorMessage="Hora final deve ser preenchida")]
        public double Hora_final { get; set; }
        public DateTime Data { get; set; }
        [Required(ErrorMessage="Situacao deve ser preenchida")]
        [MinLength(8, ErrorMessage="Situacao deve conter no mínimo 8 caracateres")]
        [MaxLength(50, ErrorMessage="Situacao não deve exceder 50 caracteres")]
        public string Situacao { get; set; }
    }
}