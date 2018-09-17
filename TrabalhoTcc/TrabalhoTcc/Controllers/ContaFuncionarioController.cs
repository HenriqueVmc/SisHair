using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TrabalhoTcc.Context;
using TrabalhoTcc.Helpers;
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
            if (ModelState.IsValid)
            {
                var usuario = LoginFuncionario.ValidarUsuario(loginF.Usuario, loginF.Senha);

                if (usuario != null)
                {
                    int id = usuario.Id;
                    var a = db.LoginFuncionarios.Where(end => end.Id == id).SingleOrDefault();
                    int b = a.PermissoesId;

                    if (b == 1)
                    {
                        permissao = "Administrador";
                    }
                    else if (b != 1)
                    {
                        permissao = "Funcionario";
                    }

                    //FormsAuthentication.SetAuthCookie(loginF.Usuario, false);
                    var ticket = FormsAuthentication.Encrypt(new FormsAuthenticationTicket(1, loginF.Usuario, DateTime.Now, DateTime.Now.AddHours(12), false, permissao));
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticket);
                    Response.Cookies.Add(cookie);


                    Session["FuncionarioId"] = usuario.FuncionarioId;

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Agendamentos", "Estatisticas");
                }
                else
                {
                    ModelState.AddModelError("", "Login Inválido");
                }
            }

            return View(loginF);
        }

        [HttpGet]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "ContaFuncionario");
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
                senha = CriptoHelper.HashMD5(senha);
                LoginFuncionario loginAntigo = db.LoginFuncionarios.SingleOrDefault(lf => lf.Usuario == usuario && lf.Senha == senha);

                if (loginAntigo != null)
                {
                    loginAntigo.Usuario = novoUsuario;
                    loginAntigo.Senha = CriptoHelper.HashMD5(novaSenha);

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

        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Index()
        {
            return View(await db.LoginFuncionarios.ToListAsync());
        }

        [Authorize(Roles = "Administrador")]
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



        [HttpGet]
        public ActionResult GetPermissoes(string term)
        {
            var permissoes = db.permissoes.ToList();

            if (term != null)
            {
                permissoes = db.permissoes.Where(p => p.TipoPermissao.ToLower().StartsWith(term.ToLower())).ToList();
            }

            var Results = permissoes.Select(p => new
            {
                text = p.TipoPermissao,
                id = p.Id

            });

            return Content(JsonConvert.SerializeObject(new { items = Results }));
            //return Json(Results, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult EsqueciMinhaSenha(string email, string CpfLogin)
        {
            if (HttpContext.Request.HttpMethod.ToUpper() == "GET")
            {
                ViewBag.EmailEnviado = false;
                ModelState.Clear();
            }
            else
            {
                var usuario = LoginFuncionario.RecuperarUsuario(email, CpfLogin);
                if (usuario != null)
                {
                    EnviarEmailRedefinicaoSenha(usuario);
                    ViewBag.EmailEnviado = true;
                }
                else
                {
                    ViewBag.EmailEnviado = false;
                    ModelState.AddModelError("", "Dados inválidos!");                  
                }
            }

            return View();
        }

        private void EnviarEmailRedefinicaoSenha(LoginFuncionario usuario)
        {
            var callbackUrl = Url.Action("RedefinirSenha", "ContaFuncionario", new { id = usuario.Id }, protocol: Request.Url.Scheme);

            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("salaosuporte@gmail.com", "suporteadm");
            smtp.EnableSsl = true;


            mail.From = new MailAddress("salaosuporte@gmail.com");
            mail.To.Add(usuario.Funcionario.Email);
            mail.Subject = "Redefinição de senha";
            mail.Body = string.Format("Redefina a sua senha <a href='{0}'>clicando aqui!</a>", callbackUrl);
            mail.IsBodyHtml = true;

            smtp.Send(mail);
        }

        [AllowAnonymous]
        public ActionResult RedefinirSenha(int id)
        {
            var usuario = LoginFuncionario.RecuperarPeloId(id);
            if (usuario == null)
            {
                id = -1;
            }

            ViewBag.Mensagem = null;

            return View(usuario);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult RedefinirSenha(LoginFuncionario login)
        {
            ViewBag.Mensagem = null;

            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var usuario = LoginFuncionario.RecuperarPeloId(login.Id);
            if (usuario != null)
            {
                usuario.Senha = CriptoHelper.HashMD5(login.Senha);
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();

                ViewBag.Mensagem = "Sucesso!";
            }

            return View();
        }
    }
}