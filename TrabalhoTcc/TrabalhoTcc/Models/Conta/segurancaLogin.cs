using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrabalhoTcc.Models.Conta
{
    public class segurancaLogin
    {
        [Key]
        public int Id { get; set; }

        
        public int IdUsuario { get; set; }

        public string EmailUsuario { get; set; }

        public int Quantidade { get; set; }
    }
}