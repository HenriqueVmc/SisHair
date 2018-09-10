﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TrabalhoTcc.Context;

namespace TrabalhoTcc.Models
{
    public class ServicosAgendamento
    {
        private DBContext db = new DBContext();

        [Key]
        public int Id { get; set; }

        public virtual Agendamento Agendamento { get; set; }
        public int AgendamentoId { get; set; }

        public virtual Servico Servico { get; set; }
        public int ServicoId { get; set; }

        public string salvarServicosAgendamento(Agendamento a, List<int> servicos)
        {
            
            foreach (int idServico in servicos)
            {
                var servico = db.Servicos.Where(s => s.Id == idServico).SingleOrDefault();

                ServicosAgendamento sa = new ServicosAgendamento();
                sa.AgendamentoId = a.Id;
                sa.ServicoId = servico.Id;

                db.ServicosAgendamento.Add(sa);                

                a.Descricao += (string.IsNullOrEmpty(a.Descricao)) ? sa.Servico.Nome : ", " + sa.Servico.Nome;
                db.SaveChanges();

            }
            return a.Descricao;
        }
    }
}