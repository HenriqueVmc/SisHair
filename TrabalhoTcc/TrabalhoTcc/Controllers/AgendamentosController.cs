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
            ViewBag.FuncionarioId = new SelectList(db.Funcionarios, "Id", "Nome");
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nome");
            return View();
        }

        [HttpGet]
        public JsonResult GetAgendamentos()
        {
            var agendamentos = db.Agendamentos.Select(a => new { a.Id, a.DataHoraInicio, a.DataHoraFinal, a.FuncionarioId, Funcionario = a.Funcionario.Nome, a.ClienteId, Cliente = a.Cliente.Nome, a.Situacao }).ToList();

            return new JsonResult { Data = agendamentos, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult Salvar(Agendamento a)
        {
            var status = false;
            try
            {
                if (a.Id > 0)
                {
                    //Update the Agendamento
                    var result = db.Agendamentos.Where(agen => agen.Id == a.Id).FirstOrDefault();
                    if (result != null)
                    {
                        result.DataHoraInicio = a.DataHoraInicio;
                        result.DataHoraFinal = a.DataHoraFinal;
                        result.Situacao = a.Situacao;
                        result.FuncionarioId = a.FuncionarioId;
                        result.ClienteId = a.ClienteId;
                    }
                }
                else
                {
                    db.Agendamentos.Add(a);
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

            var agendamento = new Agendamento(){
                DataHoraInicio = s.DataHoraInicio,
                DataHoraFinal = s.DataHoraFinal,
                FuncionarioId = s.FuncionarioId,
                ClienteId = s.ClienteId,
                Situacao = "Agendado"
            };

            db.Agendamentos.Add(agendamento);        
            db.SaveChanges();

            db.Solicitacoes.Remove(s);
            db.SaveChanges();

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
    }
}