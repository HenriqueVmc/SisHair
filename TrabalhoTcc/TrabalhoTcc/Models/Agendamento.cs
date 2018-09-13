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

        [Required(ErrorMessage="Horários devem ser preenchidos")]
        [Display(Name = "Horário de Inicio:")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DataHoraInicio { get; set; }

        [Required(ErrorMessage = "Horários devem ser preenchidos")]
        [Display(Name = "Horário de Finalização:")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DataHoraFinal { get; set; }

        //[Required(ErrorMessage="Situacao deve ser preenchida")]
        //[MinLength(8, ErrorMessage="Situacao deve conter no mínimo 8 caracateres")]
        //[MaxLength(50, ErrorMessage="Situacao não deve exceder 50 caracteres")]
        [Display(Name = "Situação: ")]
        public string Situacao { get; set; }

        [Display(Name = "Descrição: ")]
        public string Descricao { get; set; }

        [Display(Name = "Serviços: ")]
        public string Servicos { get; set; }

        [Display(Name = "Funcionário: ")]
        public virtual Funcionario Funcionario { get; set; }
        public int FuncionarioId { get; set; }
        
        [Display(Name = "Cliente: ")]
        public virtual Cliente Cliente { get; set; }
        public int ClienteId { get; set; }

        //public ICollection<Servico> Servicos { get; set; }
       
    }
}