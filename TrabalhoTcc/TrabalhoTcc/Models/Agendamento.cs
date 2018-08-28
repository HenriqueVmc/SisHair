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
        //[Required(ErrorMessage="Hora deve ser preenchida")]
        [Display(Name = "Hora Inicial:")]
        [DisplayFormat(DataFormatString = "{0: t}")]
        public DateTime HoraInicio { get; set; }
        //[Required(ErrorMessage="Hora final deve ser preenchida")]
        [Display (Name = "Hora Final:")]
        [DisplayFormat(DataFormatString = "{0: t}")]
        public DateTime HoraFinal { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }
        //[Required(ErrorMessage="Situacao deve ser preenchida")]
        //[MinLength(8, ErrorMessage="Situacao deve conter no mínimo 8 caracateres")]
        //[MaxLength(50, ErrorMessage="Situacao não deve exceder 50 caracteres")]
        [Display(Name ="Situação: ")]
        public string Situacao { get; set; }

        [Display(Name = "Funcionário: ")]
        public virtual Funcionario Funcionario { get; set; }
        public int FuncionarioId { get; set; }

        [Display(Name = "Cliente: ")]
        public virtual Cliente Cliente { get; set; }
        public int ClienteId { get; set; }

        public virtual ICollection<Servico> Servicos { get; set; }
        public int ServicoId { get; set; }

    }
}