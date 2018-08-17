using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrabalhoTcc.Models
{
    public class Solicitacoes
    {
        public int Id { get; set; }

        public DateTime Hora_inicio { get; set; }
        
        public DateTime Data { get; set; }
        [Required(ErrorMessage="Situacao deve ser preenchida")]
        [MinLength(7, ErrorMessage="Situacao deve conter no mínimo 7 caracteres")]
        [MaxLength(50, ErrorMessage="Solicitacao nao deve exceder 50 caracteres")]
        public string Situacao { get; set; }
    
    }
}