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
    public class ServicosController : Controller
    {
        private DBContext db = new DBContext();

        [Authorize(Roles = "Administrador, Funcionario")]
        // GET: Servicoes
        public async Task<ActionResult> Index()
        {
            return View(await db.Servicos.ToListAsync());
        }

        // GET: Servicoes/Details/5
        [Authorize(Roles = "Administrador, Funcionario")]
        public async Task<ActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servico servico = await db.Servicos.FindAsync(id);
            if (servico == null)
            {
                return HttpNotFound();
            }
            return View(servico);
        }

        // GET: Servicoes/Create
        [Authorize(Roles = "Administrador, Funcionario")]
        public ActionResult Cadastrar()
        {
            return View();
        }

        // POST: Servicoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Cadastrar([Bind(Include = "Id,Nome,Valor,Duracao,Descricao")] Servico servico)
        {
            if (ModelState.IsValid)
            {
                db.Servicos.Add(servico);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(servico);
        }

        // GET: Servicoes/Edit/5
        [Authorize(Roles = "Administrador, Funcionario")]
        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servico servico = await db.Servicos.FindAsync(id);
            if (servico == null)
            {
                return HttpNotFound();
            }
            return View(servico);
        }

        // POST: Servicoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar([Bind(Include = "Id,Nome,Valor,Duracao,Descricao")] Servico servico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servico).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(servico);
        }

        // GET: Servicoes/Delete/5
        [Authorize(Roles = "Administrador, Funcionario")]
        public async Task<ActionResult> Deletar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servico servico = await db.Servicos.FindAsync(id);
            if (servico == null)
            {
                return HttpNotFound();
            }
            return View(servico);
        }

        // POST: Servicoes/Delete/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Servico servico = await db.Servicos.FindAsync(id);
            db.Servicos.Remove(servico);
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


        [HttpGet]
        public ActionResult GetServicos(string term)
        {
            var servicos = db.Servicos.ToList();

            if (term != null)
            {
                servicos = db.Servicos.Where(s => s.Nome.ToLower().StartsWith(term.ToLower())).ToList();
            }

            var Results = servicos.Select(s => new
            {
                text = s.Nome,
                id = s.Id

            });
            return Content(JsonConvert.SerializeObject(new { items = Results }));
            //return Json(Results, JsonRequestBehavior.AllowGet);
        }
    }
}
