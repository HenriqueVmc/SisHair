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
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

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

            ViewBag.Endereco = db.EnderecoFuncionarios.Where(end => end.Funcionario.Id == id).SingleOrDefault();

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
                db.SaveChanges();

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

            HtmlHelper.ClientValidationEnabled = true;
            HtmlHelper.UnobtrusiveJavaScriptEnabled = true;

            Funcionario funcionario = await db.Funcionarios.FindAsync(id);

            ViewBag.Endereco = db.EnderecoFuncionarios.Where(end => end.Funcionario.Id == funcionario.Id).SingleOrDefault();

            // --- Iria trazer as permissões tbem...
            int idPermissao = db.LoginFuncionarios.Where(p => p.FuncionarioId == funcionario.Id).SingleOrDefault().PermissoesId;
            ViewBag.Permissao = db.permissoes.Where(p => p.Id == idPermissao).SingleOrDefault();

            if (funcionario == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CargoId = new SelectList(db.Cargos, "Id", "Nome");
            ViewBag.CargoId = new SelectList(db.Cargos, "Id", "Nome", funcionario.CargoId);
            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Editar(Funcionario funcionario, EnderecoFuncionario endereco, int PermissoesId)
        {
            if (ModelState.IsValid)
            {
                db.Entry(funcionario).State = EntityState.Modified;
                db.SaveChanges();

                if (endereco != null)
                {
                    endereco.FuncionarioId = funcionario.Id;
                    new EnderecoFuncionario().EditarEndereco(endereco);
                }
                LoginFuncionario login = db.LoginFuncionarios.Where(p => p.FuncionarioId == funcionario.Id).SingleOrDefault();
                login.PermissoesId = PermissoesId;

                db.Entry(login).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            HtmlHelper.ClientValidationEnabled = true;
            HtmlHelper.UnobtrusiveJavaScriptEnabled = true;
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

        public ActionResult ExportExcel()
        {
            var list = db.Funcionarios.ToList();
            var gv = new GridView();

            gv.DataSource = list;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=PlanilhaSisHAIR(" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm") + ").xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();

            return View();
        }
    }
}
