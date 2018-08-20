using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrabalhoTcc.Models
{
    public class LoginCliente
    {
        [Key]
        public int Id { get; set; }

        public string Usuario { get; set; }

        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public int Id_Cliente { get; set; }
        public Cliente Cliente { get;set; }
    }
}