using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TrabalhoTcc.Context;
using TrabalhoTcc.Models.Conta;

namespace TrabalhoTcc.Controllers
{
    public class ContaFuncionarioController : Controller
    {
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
                FormsAuthentication.SetAuthCookie(loginF.Usuario, false);

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Adm");
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
                LoginFuncionario loginAntigo = db.LoginFuncionarios.Where(lf => lf.Usuario == usuario && lf.Senha == senha).FirstOrDefault();

                var NovoLogin = new LoginFuncionario(){
                    Usuario = usuario,
                    Senha = senha,
                    FuncionarioId = loginAntigo.FuncionarioId
                };

                db.Entry(NovoLogin).State = EntityState.Modified;
                db.SaveChanges();

                FormsAuthentication.SetAuthCookie(NovoLogin.Usuario, false);
                return RedirectToAction("Index", "Adm");
            }
            else
            {
                ModelState.AddModelError("", "Login Inválido");
            }

            return View();
        }
    }
}