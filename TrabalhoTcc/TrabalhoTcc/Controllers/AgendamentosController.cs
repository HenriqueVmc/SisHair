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

namespace TrabalhoTcc.Controllers
{
    public class AgendamentosController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Agendamentos
        public async Task<ActionResult> Index()
        {
            var agendamentos = db.Agendamentos.Include(a => a.Cliente).Include(a => a.Funcionario);
            return View(await agendamentos.ToListAsync());
        }

        // GET: Agendamentos/Details/5
        public async Task<ActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agendamento agendamento = await db.Agendamentos.FindAsync(id);
            if (agendamento == null)
            {
                return HttpNotFound();                
            }
            return View(agendamento);
        }

        // GET: Agendamentos/Create
        public ActionResult Cadastrar()
        {
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nome");
            ViewBag.FuncionarioId = new SelectList(db.Funcionarios, "Id", "Nome");
            ViewBag.ServicoId = new SelectList(db.Servicos, "Id", "Nome");
            return View();
        }

        // POST: Agendamentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Cadastrar([Bind(Include = "Id,HoraInicio,HoraFinal,Data,Situacao,FuncionarioId,ClienteId,ServicoId")] Agendamento agendamento)
        {
            if (ModelState.IsValid)
            {
                db.Agendamentos.Add(agendamento);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nome", agendamento.ClienteId);
            ViewBag.FuncionarioId = new SelectList(db.Funcionarios, "Id", "Nome", agendamento.FuncionarioId);
            return View(agendamento);
        }

        // GET: Agendamentos/Edit/5
        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agendamento agendamento = await db.Agendamentos.FindAsync(id);
            if (agendamento == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nome", agendamento.ClienteId);
            ViewBag.FuncionarioId = new SelectList(db.Funcionarios, "Id", "Nome", agendamento.FuncionarioId);
            return View(agendamento);
        }

        // POST: Agendamentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar([Bind(Include = "Id,HoraInicio,HoraFinal,Data,Situacao,FuncionarioId,ClienteId,ServicoId")] Agendamento agendamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agendamento).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nome", agendamento.ClienteId);
            ViewBag.FuncionarioId = new SelectList(db.Funcionarios, "Id", "Nome", agendamento.FuncionarioId);
            return View(agendamento);
        }

        // GET: Agendamentos/Delete/5
        public async Task<ActionResult> Deletar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agendamento agendamento = await db.Agendamentos.FindAsync(id);
            if (agendamento == null)
            {
                return HttpNotFound();
            }
            return View(agendamento);
        }

        // POST: Agendamentos/Delete/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Agendamento agendamento = await db.Agendamentos.FindAsync(id);
            db.Agendamentos.Remove(agendamento);
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
