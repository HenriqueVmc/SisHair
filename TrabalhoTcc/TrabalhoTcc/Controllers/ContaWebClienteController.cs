using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabalhoTcc.Context;
using TrabalhoTcc.Models;

namespace TrabalhoTcc.Controllers
{
    public class ContaWebClienteController : Controller
    {
        // GET: ContaWebCliente
        public ActionResult Index()
        {
            using (DBContext db = new DBContext())
            {
                return View(db.contacliente.ToList());
            }
        }


        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastro(ClienteN cliente)
        {
            if (ModelState.IsValid)
            {
                using (DBContext db = new DBContext())
                {
                    db.contacliente.Add(cliente);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = cliente.Nome + " registrado com sucesso";
            }
            return View();
        }



        //LOGIN

        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(ClienteN usuario)
        {
            using (DBContext db = new DBContext())
            {
                var usr = db.contacliente.Single(u => u.Email == usuario.Email && u.Senha == usuario.Senha);
                if (usr != null)
                {
                    Session["Id"] = usr.Id.ToString();
                    Session["Email"] = usr.Email.ToString();
                    return RedirectToAction("Logado");
                }
                else
                {
                    ModelState.AddModelError("", "Email ou senha incorreta");
                }
            }
            return View();
        }

        public ActionResult Logado()
        {
            if (Session["Id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }

        }
    }
}