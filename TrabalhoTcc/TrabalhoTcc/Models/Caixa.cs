using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrabalhoTcc.Models
{
    public class Caixa
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Valor Total: ")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal ValorTotal { get; set; }

        [Display(Name = "Valor Pago: ")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal ValorPago { get; set; }

        [Display(Name = "Dívida: ")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Divida { get; set; }

        [Display(Name = "Forma de Pagamento: ")]
        public string FormaPagamento { get; set; }

        [Display(Name = "Data de Pagamento:")]
        [DataType(DataType.Date)]
        public DateTime DataPagamento { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        public virtual Agendamento Agendamento { get; set; }
        public int AgendamentoId { get; set; }

    }
}