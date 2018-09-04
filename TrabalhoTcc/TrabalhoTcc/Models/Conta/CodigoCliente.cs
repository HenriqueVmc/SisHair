using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrabalhoTcc.Models.Conta
{
    public class CodigoCliente
    {
        public int Id { get; set; }
        public int Id_Usuario { get; set; }
        public string Email { get; set; }
        public string Codigo { get; set; }
    }
}