using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using TrabalhoTcc.Context;
using TrabalhoTcc.Helpers;
using TrabalhoTcc.Models;
using TrabalhoTcc.Models.Conta;

namespace TrabalhoTcc.Controllers
{
    public class ContaClienteController : Controller
    {
        private DBContext db = new DBContext();

        // GET: ContaCliente
        public ActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginCliente loginC, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = LoginCliente.ValidarUsuario(loginC.Usuario, loginC.Senha);

                    if (usuario != null)
                    {
                        Session["ClienteId"] = usuario.ClienteId;
                        return RedirectToAction("Agendamento", "Solicitacao");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Login Inválido");
                    }
                }
                catch (Exception e) { ModelState.AddModelError("", "Confira os dados e tente novamente"); }
            }

            return View(loginC);
        }

        [HttpGet]
        public ActionResult CriarConta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CriarConta(Cliente cliente, LoginCliente loginC)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    string VEmail = cliente.Email;
                    var ValidarEmail = db.Clientes.Where(a => a.Email == VEmail).SingleOrDefault();
                    if (ValidarEmail != null)
                    {
                        ModelState.AddModelError("", "Esse Cadastro já Existe!");
                    }
                    else
                    {
                        if (!(LoginCliente.Existe(loginC)))
                        {
                            db.Clientes.Add(cliente);

                            var LoginCliente1 = new LoginCliente()
                            {
                                Usuario = loginC.Usuario,
                                Senha = CriptoHelper.HashMD5(loginC.Senha),
                                ClienteId = cliente.Id
                            };

                            db.LoginClientes.Add(LoginCliente1);

                            db.SaveChanges();
                            return RedirectToAction("Login", "ContaCliente");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Esse usuário já existe!");
                        }
                    }
                }
                catch (Exception e) { ModelState.AddModelError("", "Confira os dados e tente novamente"); }
            }
            HtmlHelper.ClientValidationEnabled = true;
            HtmlHelper.UnobtrusiveJavaScriptEnabled = true;

            ViewBag.cliente = cliente;
            return View();
        }


        //////////////////////////Recuperar Password///////////////////////////
        bool r = false;
        [HttpGet]
        public ActionResult EmailCliente()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EmailCliente(string email)
        {
            try
            {
                Cliente cli = db.Clientes.Where(c => c.Email == email).SingleOrDefault();

                if (cli != null)
                {
                    PassarMensagem error = new PassarMensagem();
                    error.ErrorMessage = email;
                    error.Codigo = cli.Id;
                    TempData["Error"] = error;

                    return RedirectToAction("CadastrarCodigo");
                }
                else
                {
                    ModelState.AddModelError("", "Email Invalido");
                }
            }
            catch (Exception e) { ModelState.AddModelError("", "Confira os dados e tente novamente"); }

            return View();
        }

        public ActionResult CadastrarCodigo(CodigoCliente codCliente)
        {
            Random rdn = new Random();
            int codigo = rdn.Next(100000, 999999);
            PassarMensagem error = TempData["error"] as PassarMensagem;
            ViewData["ErrorMensagem"] = error.ErrorMessage;
            ViewData["Codigo"] = error.Codigo;
            error.CodigoVerdadeiro = Convert.ToString(codigo);
            codCliente.Email = error.ErrorMessage;
            codCliente.Id_Usuario = error.Codigo;
            codCliente.Codigo = Convert.ToString(codigo);


            if (ModelState.IsValid)
            {
                db.CodigosClientes.Add(codCliente);
                db.SaveChangesAsync();
                return RedirectToAction("EnviarEmail");

            }
            return View();
        }

        public ActionResult EnviarEmail()
        {

            try
            {
                PassarMensagem error = TempData["error"] as PassarMensagem;
                ViewData["ErrorMensagem"] = error.ErrorMessage;
                ViewData["Codigo"] = error.Codigo;
                string email = error.ErrorMessage;
                int id = error.Codigo;
                string codigo = error.CodigoVerdadeiro;

                string assunto = "Este e seu código para redifinição de senha: " + codigo;

                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("salaosuporte@gmail.com");
                mail.To.Add(email);
                mail.Subject = assunto;

                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("salaosuporte@gmail.com", "suporteadm");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return RedirectToAction("VerificarCodigo");

            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult VerificarCodigo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VerificarCodigo(string CodigoRetorno)
        {
            try
            {
                if (CodigoRetorno != null)
                {
                    CodigoCliente codCliente = db.CodigosClientes.Where(c => c.Codigo == CodigoRetorno).SingleOrDefault();

                    if (codCliente != null)
                    {

                        return RedirectToAction("RedefinirSenha");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Codigo Invalido");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Codigo Invalido");
                }
            }
            catch (Exception e) { ModelState.AddModelError("", "Confira os dados e tente novamente"); }
            return View();
        }

        [HttpGet]
        public ActionResult RedefinirSenha()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RedefinirSenha(string Senha, string ConfirmarSenha)
        {
            try
            {
                if (Senha == ConfirmarSenha)
                {
                    PassarMensagem error = TempData["error"] as PassarMensagem;
                    int id = error.Codigo;
                    Cliente cli = db.Clientes.Where(c => c.Id == id).SingleOrDefault();

                    if (cli != null)
                    {
                        LoginCliente log = db.LoginClientes.Where(d => d.ClienteId == id).SingleOrDefault();
                        if (log != null)
                        {
                            log.Senha = Senha;
                            string codigo = error.CodigoVerdadeiro;
                            CodigoCliente cod = db.CodigosClientes.Where(d => d.Codigo == codigo).SingleOrDefault();
                            db.CodigosClientes.Remove(cod);
                            db.SaveChanges();
                            return RedirectToAction("Login");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Senhas não conferem");
                }
            }
            catch (Exception e) { ModelState.AddModelError("", "Confira os dados e tente novamente"); }
            return View();
        }

    }
}