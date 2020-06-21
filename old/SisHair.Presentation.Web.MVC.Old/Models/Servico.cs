using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SisHair.Presentation.Web.MVC.Old.Models
{
    public class Servico
    {        
        [Key]
        public int Id { get; set; }
        [Display(Name ="Serviço: ")]
        //[Required(ErrorMessage = "Serviço não pode ser vasio")]
        public string Nome { get; set; }

        //[Required(ErrorMessage = "Valor não pode ser vasio")]
        [Display(Name = "Valor: ")]
        [DisplayFormat(DataFormatString = "{0:c}")]
       // [Column(TypeName = "money")]
        public decimal Valor { get; set; }

        //[Required(ErrorMessage = "Duraçao não pode ser vasio")]
        [Display(Name = "Duração: ")]
        public int Duracao { get; set; }

        //[Required(ErrorMessage = "Descriçao não pode ser vasio")]
        [Display(Name = "Descrição: ")]
        public string Descricao { get; set; }
        [DefaultValue(true)]
        public bool RegistroServicoAtivo { get; set; }
    }
}