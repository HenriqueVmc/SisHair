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

        public string Situacao { get; set; }
    
    }
}