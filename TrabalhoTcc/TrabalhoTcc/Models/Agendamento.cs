using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrabalhoTcc.Models
{
    public class Agendamento
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage="Hora deve ser preenchida")]
        public DateTime Hora_inicio { get; set; }
        [Required(ErrorMessage="Hora final deve ser preenchida")]
        public DateTime Hora_final { get; set; }
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        public virtual ICollection<Servicos> Servicos { get; set; }
        public virtual Funcionario Funcionario { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}