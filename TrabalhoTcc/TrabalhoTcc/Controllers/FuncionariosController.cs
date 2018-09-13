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
using TrabalhoTcc.Models.Conta;
using Newtonsoft.Json;
using TrabalhoTcc.Helpers;

namespace TrabalhoTcc.Controllers
{
    public class FuncionariosController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Funcionarios       
        [Authorize(Roles = "Administrador, Funcionario")]
        public ActionResult Index()
        {
            var funcionarios = db.Funcionarios.Include(f => f.Cargo);
            return View(funcionarios);
        }

        // GET: Funcionarios/Details/5
         [Authorize(Roles = "Administrador, Funcionario")]
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
        [Authorize(Roles = "Administrador")]
        public ActionResult Cadastrar()
        {

            HtmlHelper.ClientValidationEnabled = true;
            HtmlHelper.UnobtrusiveJavaScriptEnabled = true;
            ViewBag.CargoId = new SelectList(db.Cargos, "Id", "Nome");
            ViewBag.PermissoesId = new SelectList(db.permissoes, "Id", "TipoPermissao");
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Funcionario funcionario, EnderecoFuncionario endereco, int PermissoesId)
        {
            if (ModelState.IsValid)
            {
                db.Funcionarios.Add(funcionario);
   
                if (endereco != null)
                {
                    new EnderecoFuncionario().CadastrarEndereco(endereco, funcionario.Id);
                }
         
                ////
                var loginF = new LoginFuncionario()
                {
                    Usuario = funcionario.Email,
                    Senha = gerarSenha(funcionario),
                    FuncionarioId = funcionario.Id,
                    PermissoesId = PermissoesId
                };
                ////
                db.LoginFuncionarios.Add(loginF);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            HtmlHelper.ClientValidationEnabled = true;
            HtmlHelper.UnobtrusiveJavaScriptEnabled = true;
            ViewBag.CargoId = new SelectList(db.Cargos, "Id", "Nome", funcionario.CargoId);
            return View(funcionario);
        }


        private string gerarSenha(Funcionario funcionario)
        {
            string senha = Convert.ToString(funcionario.DataNascimento.Day.ToString().PadLeft(2, '0'));
            senha += Convert.ToString(funcionario.DataNascimento.Month.ToString().PadLeft(2, '0'));
            senha += Convert.ToString(funcionario.DataNascimento.Year);

            return CriptoHelper.HashMD5(senha);
        }








        // GET: Funcionarios/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcionario funcionario = await db.Funcionarios.FindAsync(id);

            ViewBag.Endereco = db.EnderecoFuncionarios.Where(end => end.Funcionario.Id == funcionario.Id).SingleOrDefault();

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
        [Authorize(Roles = "Administrador")]
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

        [HttpGet]
        public ActionResult GetFuncionarios(string term)
        {
            var funcionarios = db.Funcionarios.ToList();

            if (term != null)
            {
                funcionarios = db.Funcionarios.Where(s => s.Nome.ToLower().StartsWith(term.ToLower())).ToList();
            }

            var Results = funcionarios.Select(s => new
            {
                text = s.Nome,
                id = s.Id

            });

            return Content(JsonConvert.SerializeObject(new { items = Results }));
            //return Json(Results, JsonRequestBehavior.AllowGet);
        }

    }
}
