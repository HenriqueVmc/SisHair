using Newtonsoft.Json;
using TrabalhoTcc.Context;
using TrabalhoTcc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;

namespace TrabalhoTcc.Controllers
{
    public class AgendamentosController : Controller
    {
        private DBContext db = new DBContext();
        // GET: Agendamentos
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAgendamentos()//int? id
        {
            var agendamentos = db.Agendamentos.Select(a => new { a.Id, a.DataHoraInicio, a.DataHoraFinal, a.FuncionarioId, Funcionario = a.Funcionario.Nome, a.ClienteId, Cliente = a.Cliente.Nome, a.Situacao, a.Descricao, a.Servicos }).ToList();//.Where(a => a.FuncionarioId == id)

            return new JsonResult { Data = agendamentos, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [HttpPost]
        public JsonResult Salvar([Bind(Include = "Id,DataHoraInicio,DataHoraFinal,Descricao,Situacao,ClienteId,FuncionarioId")]Agendamento a, List<int> servicos)
        {
            var status = false;
            try
            {
                if (a.Id > 0)
                {
                    //Update no Agendamento
                    var agendamento = db.Agendamentos.Where(agen => agen.Id == a.Id).FirstOrDefault();

                    if (agendamento != null)
                    {
                        agendamento.DataHoraInicio = a.DataHoraInicio;
                        agendamento.DataHoraFinal = a.DataHoraFinal;
                        agendamento.Situacao = a.Situacao;
                        agendamento.FuncionarioId = a.FuncionarioId;
                        agendamento.ClienteId = a.ClienteId;
                        agendamento.Descricao = a.Descricao;

                        if (servicos != null)
                        {
                            var servicosAgendamento = db.ServicosAgendamento.Where(sa => sa.AgendamentoId == a.Id).ToList();
                            if (servicosAgendamento != null)
                            {
                                foreach (ServicosAgendamento servicoA in servicosAgendamento)
                                {
                                    db.ServicosAgendamento.Remove(servicoA);
                                }
                            }

                            agendamento.Servicos = null;
                            agendamento.Servicos = new ServicosAgendamento().salvarServicosAgendamento(agendamento, servicos);
                        }
                    }

                }
                else
                {
                    db.Agendamentos.Add(a);

                    if (servicos != null)
                    {
                        foreach (int idServico in servicos)
                        {
                            var servico = db.Servicos.Where(s => s.Id == idServico).SingleOrDefault();

                            ServicosAgendamento sa = new ServicosAgendamento();
                            sa.AgendamentoId = a.Id;
                            sa.ServicoId = servico.Id;

                            db.ServicosAgendamento.Add(sa);

                            a.Servicos += (string.IsNullOrEmpty(a.Servicos)) ? sa.Servico.Nome : ", " + sa.Servico.Nome;
                        }
                    }
                }
                db.SaveChanges();
                status = true;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult Deletar(int id)
        {
            var status = false;

            var result = db.Agendamentos.Where(agen => agen.Id == id).FirstOrDefault();
            if (result != null)
            {
                db.Agendamentos.Remove(result);
                db.SaveChanges();
                status = true;
            }

            return new JsonResult { Data = new { status = status } };
        }


        public ActionResult Solicitacoes()
        {
            var solicitacoes = db.Solicitacoes.ToList();
            return View(solicitacoes);
        }

        [HttpGet]
        public ActionResult SalvarSolicitacao(int id)
        {
            var s = db.Solicitacoes.Where(soli => soli.Id == id).SingleOrDefault();

            var agendamento = new Agendamento()
            {
                DataHoraInicio = s.DataHoraInicio,
                DataHoraFinal = s.DataHoraFinal,
                FuncionarioId = s.FuncionarioId,
                ClienteId = s.ClienteId,
                Situacao = "Confirmado",
                Descricao = s.Descricao,
                Servicos = s.Servicos
            };

            db.Agendamentos.Add(agendamento);

            s.Situacao = "Confirmado";
            db.Entry(s).State = EntityState.Modified;
            
            db.SaveChanges();

            var servicosSolicitacao = db.ServicosSolicitacao.Where(ss => ss.SolicitacaoId == id).Select(ss => new { ss.ServicoId }).ToList();

            if (servicosSolicitacao != null)
            {
                foreach (var ss in servicosSolicitacao)
                {
                    var servico = db.Servicos.Where(ser => ser.Id == ss.ServicoId).SingleOrDefault();

                    ServicosAgendamento sa = new ServicosAgendamento();
                    sa.AgendamentoId = agendamento.Id;
                    sa.ServicoId = servico.Id;

                    db.ServicosAgendamento.Add(sa);
                    db.SaveChanges();
                }
            }

            //s.Situacao = "Confirmado";            

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult CancelarSolicitacao(int id)
        {
            var s = db.Solicitacoes.Where(soli => soli.Id == id).SingleOrDefault();

            db.Solicitacoes.Remove(s);
            db.SaveChanges();

            return RedirectToAction("Solicitacoes");
        }

        
        [HttpGet]
        public JsonResult GetAgendamentosByMes()
        {
            //SELECT MONTH(DataHoraFinal), COUNT(DataHoraFinal) FROM Agendamentoes WHERE YEAR(DataHoraFinal) = '2018' GROUP BY  MONTH(DataHoraFinal);
            var x = from ag in db.Agendamentos
                    where ag.DataHoraFinal.Year == 2018
                    group ag by ag.DataHoraFinal.Month into groupmonth
                    select new { Quantidade = groupmonth.Count(), 
                        Mes = groupmonth.Key};
            var registros = x.ToList();

            var teste = x.ToArray();
            var valores = new int[12];
            foreach (var y  in registros)
            {
                valores[(y.Mes)-1] = y.Quantidade;
            }
            //var agendamentosbyMes = db.Agendamentos.Where(a => a.DataHoraFinal.Year == DateTime.Now.Year).GroupBy(a => a.DataHoraFinal.Month).Count();            

            return new JsonResult { Data = valores , JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}