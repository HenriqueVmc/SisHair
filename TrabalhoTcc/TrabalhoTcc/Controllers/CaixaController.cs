using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrabalhoTcc.Context;
using TrabalhoTcc.Models;
using Newtonsoft.Json;

namespace TrabalhoTcc.Controllers
{
    public class CaixaController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Caixa
        public async Task<ActionResult> Index()
        {
            var caixa = db.Caixa.Include(c => c.Agendamento);
            return View(await caixa.ToListAsync());
        }

        // GET: Caixa/Create
        public ActionResult Pagamento(int? id)
        {
            if (id > 0)
            {
                /*            
                SELECT a.Descricao, SUM(s.Valor) AS 'Valor Total' FROM ServicosAgendamentoes sa
                LEFT JOIN Agendamentoes a ON sa.AgendamentoId = a.Id
                JOIN Servicoes s ON sa.ServicoId = s.Id GROUP BY a.Descricao
                */
                ViewBag.ValorTotal = db.ServicosAgendamento.Where(sa => sa.AgendamentoId == id).Include(sa => sa.Servico).Select(sa => new
                {
                    sa.Servico.Valor
                }).Sum(s => s.Valor);

                ViewBag.Agendamento = db.Agendamentos.Where(a => a.Id == id).SingleOrDefault();
                
                return View();
            }

            return RedirectToAction("Index");
        }

        // POST: Caixa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Pagamento([Bind(Include = "Id,ValorTotal,ValorPago,Divida,FormaPagamento,DataPagamento,Status,AgendamentoId")] Caixa caixa)
        {
            if (ModelState.IsValid)
            {
                db.Caixa.Add(caixa);

                //Alterar Situação do Agendamento; Ou, Excluir e add no Histórico?            
                Agendamento agendamento = db.Agendamentos.Where(a => a.Id == caixa.AgendamentoId).SingleOrDefault();
                agendamento.Situacao = "Pago";

                //db.Agendamentos.Remove(agendamento);

                db.SaveChanges();

                //db.Solicitacoes.Where(s => s.DataHoraInicio == agendamento.DataHoraInicio &&
                //                           s.DataHoraFinal == agendamento.DataHoraFinal &&
                //                           s.ClienteId == agendamento.ClienteId &&
                //                           s.FuncionarioId == agendamento.FuncionarioId).SingleOrDefault();

                return RedirectToAction("Index");
            }

            ViewBag.ValorTotal = db.ServicosAgendamento.Where(sa => sa.AgendamentoId == caixa.AgendamentoId).Include(sa => sa.Servico).Select(sa => new
            {
                sa.Servico.Valor
            }).Sum(s => s.Valor);

            ViewBag.Agendamento = db.Agendamentos.Where(a => a.Id == caixa.AgendamentoId).SingleOrDefault();
            return View(caixa);
        }

        // GET: Caixa/Edit/5
        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caixa caixa = await db.Caixa.FindAsync(id);
            if (caixa == null)
            {
                return HttpNotFound();
            }

            ViewBag.Agendamento = db.Agendamentos.Where(a => a.Id == caixa.AgendamentoId).SingleOrDefault();
            ViewBag.ValorTotal = db.Caixa.Where(c => c.Id == id).Select(c => new
            {
                c.ValorTotal
            }).SingleOrDefault();

            return View(caixa);
        }

        // POST: Caixa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Editar([Bind(Include = "Id,ValorTotal,ValorPago,Divida,FormaPagamento,DataPagamento,Status,AgendamentoId")] Caixa caixa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(caixa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ValorTotal = db.ServicosAgendamento.Where(sa => sa.AgendamentoId == caixa.AgendamentoId).Include(sa => sa.Servico).Select(sa => new
            {
                sa.Servico.Valor
            }).Sum(s => s.Valor);

            ViewBag.Agendamento = db.Agendamentos.Where(a => a.Id == caixa.AgendamentoId).SingleOrDefault();
            return View(caixa);
        }

        // GET: Caixa/Delete/5
        public async Task<ActionResult> Deletar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caixa caixa = await db.Caixa.FindAsync(id);
            if (caixa == null)
            {
                return HttpNotFound();
            }

            return View(caixa);
        }

        // POST: Caixa/Delete/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Caixa caixa = await db.Caixa.FindAsync(id);
            db.Caixa.Remove(caixa);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
