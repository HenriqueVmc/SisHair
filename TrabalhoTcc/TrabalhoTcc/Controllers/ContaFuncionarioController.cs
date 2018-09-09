using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TrabalhoTcc.Context;
using TrabalhoTcc.Models.Conta;

namespace TrabalhoTcc.Controllers
{
    public class ContaFuncionarioController : Controller
    {
        string permissao = "";
        private DBContext db = new DBContext();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginFuncionario loginF, string returnUrl)
        {
            //Valida dados
            var usuario = LoginFuncionario.ValidarUsuario(loginF.Usuario, loginF.Senha);

            if (usuario != null)
            {
                var a = db.LoginFuncionarios.Where(end => end.PermissaoId == 1).Single();
                int b = a.PermissaoId;

                if (b == 1)
                {
                    permissao = "Administrador";
                }
                else if (b != 1)
                {
                    permissao = "Funcionario";
                }
                


                //FormsAuthentication.SetAuthCookie(loginF.Usuario, false);
                var ticket = FormsAuthentication.Encrypt(new FormsAuthenticationTicket(1, loginF.Usuario, DateTime.Now, DateTime.Now.AddHours(12), false, permissao ));
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticket);
                Response.Cookies.Add(cookie);

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Agendamentos");
            }
            else
            {
                ModelState.AddModelError("", "Login Inválido");
            }

            return View(loginF);
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult AlterarLogin()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AlterarLogin(string novoUsuario, string novaSenha, string usuario, string senha)//[Bind(Include = "Id,Nome,Data_nascimento,Celular,Telefone,Email")]
        {
            if (ModelState.IsValid)
            {
                LoginFuncionario loginAntigo = db.LoginFuncionarios.SingleOrDefault(lf => lf.Usuario == usuario && lf.Senha == senha);

                if (loginAntigo != null)
                {
                    loginAntigo.Usuario = novoUsuario;
                    loginAntigo.Senha = novaSenha;

                    db.Entry(loginAntigo).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return Content(JsonConvert.SerializeObject(new { id = loginAntigo.Id }));
            }
            else
            {
                ModelState.AddModelError("", "Login Inválido");
            }

            return View();
        }

        public async Task<ActionResult> Index()
        {
            return View(await db.LoginFuncionarios.ToListAsync());
        }

        public async Task<ActionResult> Deletar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginFuncionario loginFuncionario = await db.LoginFuncionarios.FindAsync(id);
            if (loginFuncionario == null)
            {
                return HttpNotFound();
            }
            return View(loginFuncionario);
        }

        // POST: ContaFuncionario/Delete/5
        [Authorize()]
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LoginFuncionario loginFuncionario = await db.LoginFuncionarios.FindAsync(id);
            db.LoginFuncionarios.Remove(loginFuncionario);
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