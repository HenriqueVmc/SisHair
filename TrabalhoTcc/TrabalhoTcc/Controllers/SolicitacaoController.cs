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
    public class SolicitacaoController : Controller
    {
        private DBContext db = new DBContext();

        [HttpGet]
        public ActionResult Agendamento(int? idCliente)
        {
            //Receber cliente e rotornar em ViewBag para campos hiddens 
            ViewBag.ClienteId = idCliente;
            ViewBag.Funcionarios = db.Funcionarios.Include(f => f.Cargo).ToList();
            return View();
        }

        [HttpGet]
        public ActionResult AgendamentoModal(int? id)
        {
            ViewBag.FuncionarioId = id;
            return View();
        }

        [HttpPost]
        public ActionResult Agendamento(Solicitacao solicitacao)
        {
            if (ModelState.IsValid)
            {
                db.Solicitacoes.Add(solicitacao);
                db.SaveChanges();
                return Content(JsonConvert.SerializeObject(new { id = solicitacao.Id }));
            }

            ViewBag.ClienteId = solicitacao.ClienteId;
            ViewBag.Funcionarios = db.Funcionarios.Include(f => f.Cargo).ToList();

            return View(solicitacao);
        }
        // GET: Solicitacao
        public async Task<ActionResult> Index()
        {
            var solicitacoes = db.Solicitacoes.Include(s => s.Cliente).Include(s => s.Funcionario);
            return View(await solicitacoes.ToListAsync());
        }

        // GET: Solicitacao/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitacao solicitacao = await db.Solicitacoes.FindAsync(id);
            if (solicitacao == null)
            {
                return HttpNotFound();
            }
            return View(solicitacao);
        }

        // GET: Solicitacao/Create
        public ActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nome");
            ViewBag.FuncionarioId = new SelectList(db.Funcionarios, "Id", "Nome");
            return View();
        }

        // POST: Solicitacao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,DataHoraInicio,DataHoraFinal,Descricao,FuncionarioId,ClienteId")] Solicitacao solicitacao)
        {
            if (ModelState.IsValid)
            {
                db.Solicitacoes.Add(solicitacao);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nome", solicitacao.ClienteId);
            ViewBag.FuncionarioId = new SelectList(db.Funcionarios, "Id", "Nome", solicitacao.FuncionarioId);
            return View(solicitacao);
        }

        // GET: Solicitacao/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitacao solicitacao = await db.Solicitacoes.FindAsync(id);
            if (solicitacao == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nome", solicitacao.ClienteId);
            ViewBag.FuncionarioId = new SelectList(db.Funcionarios, "Id", "Nome", solicitacao.FuncionarioId);
            return View(solicitacao);
        }

        // POST: Solicitacao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,DataHoraInicio,DataHoraFinal,Descricao,FuncionarioId,ClienteId")] Solicitacao solicitacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitacao).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nome", solicitacao.ClienteId);
            ViewBag.FuncionarioId = new SelectList(db.Funcionarios, "Id", "Nome", solicitacao.FuncionarioId);
            return View(solicitacao);
        }

        // GET: Solicitacao/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitacao solicitacao = await db.Solicitacoes.FindAsync(id);
            if (solicitacao == null)
            {
                return HttpNotFound();
            }
            return View(solicitacao);
        }

        // POST: Solicitacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Solicitacao solicitacao = await db.Solicitacoes.FindAsync(id);
            db.Solicitacoes.Remove(solicitacao);
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

        public ActionResult AreaDoCliente(int? idCliente)
        {
            ViewBag.ClienteId = idCliente;
            ViewBag.AgendamentosParaAvaliar = db.Agendamentos.Where(c => c.Id == idCliente).ToList();
            return View();
        }
        
        [HttpGet]
        public ActionResult AvaliacaoModal(int? id)
        {
            ViewBag.AgendamentoId = id;
            return View();
        }
    }
}
