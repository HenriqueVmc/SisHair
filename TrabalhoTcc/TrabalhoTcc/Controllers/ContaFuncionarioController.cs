﻿using Newtonsoft.Json;
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
    }
}