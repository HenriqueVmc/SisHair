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
    public class FuncionariosController : Controller
    {
        private DBContext db = new DBContext();

        [Authorize]
        // GET: Funcionarios
        public ActionResult Index()
        {
            var funcionarios = db.Funcionarios.Include(f => f.Cargo);
            return View(funcionarios);
        }

        // GET: Funcionarios/Details/5
        public async Task<ActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcionario funcionario = await db.Funcionarios.Include(f => f.Cargo).Where(f => f.Id == id).FirstAsync();

            ViewBag.Endereco = db.EnderecoFuncionarios.Where(end => end.Funcionario.Id == id).Single();

            if (funcionario == null)
            {
                return HttpNotFound();
            }
            return View(funcionario);
        }

        // GET: Funcionarios/Create
        public ActionResult Cadastrar()
        {
            ViewBag.CargoId = new SelectList(db.Cargos, "Id", "Nome");
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Cadastrar(Funcionario funcionario, EnderecoFuncionario endereco)
        {
            if (ModelState.IsValid)
            {
                db.Funcionarios.Add(funcionario);
                await db.SaveChangesAsync();
                if (endereco != null)
                {
                    new EnderecoFuncionario().CadastrarEndereco(endereco, funcionario.Id);
                }
                return RedirectToAction("Index");
            }
            ViewBag.CargoId = new SelectList(db.Cargos, "Id", "Nome", funcionario.CargoId);
            return View(funcionario);
        }

        // GET: Funcionarios/Edit/5
        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcionario funcionario = await db.Funcionarios.FindAsync(id);

            ViewBag.Endereco = db.EnderecoFuncionarios.Where(end => end.Funcionario.Id == funcionario.Id).Single();

            if (funcionario == null)
            {
                return HttpNotFound();
            }
            ViewBag.CargoId = new SelectList(db.Cargos, "Id", "Nome", funcionario.CargoId);
            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Funcionario funcionario, EnderecoFuncionario endereco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(funcionario).State = EntityState.Modified;
                db.SaveChanges();

                new EnderecoFuncionario().EditarEndereco(endereco);

                return RedirectToAction("Index");
            }
            ViewBag.CargoId = new SelectList(db.Cargos, "Id", "Nome", funcionario.CargoId);
            return View(funcionario);
        }

        // GET: Funcionarios/Delete/5
        public async Task<ActionResult> Deletar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcionario funcionario = await db.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return HttpNotFound();
            }
            return View(funcionario);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Funcionario funcionario = await db.Funcionarios.FindAsync(id);
            db.Funcionarios.Remove(funcionario);
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
