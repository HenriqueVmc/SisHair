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
        public DateTime Hora_inicio { get; set; }
        [Required(ErrorMessage="Hora final deve ser preenchida")]
        public DateTime Hora_final { get; set; }
        public DateTime Data { get; set; }
    }
}