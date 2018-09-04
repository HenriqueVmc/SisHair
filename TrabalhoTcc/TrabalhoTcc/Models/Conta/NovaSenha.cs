using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrabalhoTcc.Models.Conta
{
    public class NovaSenha
    {
        [Required(ErrorMessage = "Senha é requirida")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "Por favor confirme a senha")]
        [DataType(DataType.Password)]
        public string ConfirmarSenha { get; set; }

    }
}