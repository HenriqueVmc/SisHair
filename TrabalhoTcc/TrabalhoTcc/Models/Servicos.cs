using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrabalhoTcc.Models
{
    public class Servicos
    {
        
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Serviço deve ser preesnchido")]
        public string Servico { get; set; }

        [Required(ErrorMessage = "Valor deve ser preesnchido")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Duraçao deve ser preesnchido")]
        public DateTime Duracao { get; set; }
        
        public string Descricao { get; set; }

        public virtual ICollection<Agendamento> Agendamentos { get; set; }
    }
}