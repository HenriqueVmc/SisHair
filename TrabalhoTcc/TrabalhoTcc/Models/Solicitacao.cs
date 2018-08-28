using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrabalhoTcc.Models
{
    public class Solicitacao
    {
        [Key]
        public int Id { get; set; }
        //[Required(ErrorMessage="Hora deve ser preenchida")]
        [Display(Name = "Hora Inicial:")]
        [DisplayFormat(DataFormatString = "{0: t}")]
        public DateTime HoraInicio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }

        [Display(Name = "Descrição:")]
        public string Descricao { get; set; }

        [Display(Name = "Funcionário:")]
        public virtual Funcionario Funcionario { get; set; }
        public int FuncionarioId { get; set; }

        [Display(Name = "Cliente ")]
        public virtual Cliente Cliente { get; set; }
        public int ClienteId { get; set; }

    }
}