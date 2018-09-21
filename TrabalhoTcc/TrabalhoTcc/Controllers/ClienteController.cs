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
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

namespace TrabalhoTcc.Controllers
{
    public class ClienteController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Cliente
        [Authorize(Roles = "Administrador, Funcionario")]
        public async Task<ActionResult> Index()
        {
            return View(await db.Clientes.ToListAsync());
        }

        // GET: Cliente/Details/5
        [Authorize(Roles = "Administrador, Funcionario")]
        public async Task<ActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = await db.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Cliente/Create
        [Authorize]
        [Authorize(Roles = "Administrador, Funcionario")]
        public ActionResult Cadastrar()
        {
            return View();
        }

        // POST: Cliente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Cadastrar([Bind(Include = "Id,Nome,Data_nascimento,Celular,Telefone,Email")] Cliente cliente)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    string VEmail = cliente.Email;
                    var clienteVerificar = db.Clientes.Where(a => a.Email == VEmail).SingleOrDefault();

                    if (clienteVerificar != null)
                    {
                        ModelState.AddModelError("", "Esse Cadastro já Existe!");
                        return View(cliente);
                    }
                    else
                    {                    
                        db.Clientes.Add(cliente);

                        var loginC = new LoginCliente()
                        {
                            Usuario = cliente.Email,
                            Senha = gerarSenha(cliente),
                            ClienteId = cliente.Id
                        };

                        db.LoginClientes.Add(loginC);
                        await db.SaveChangesAsync();

                        return RedirectToAction("Index");
                    }
                }
                catch (Exception e) { ModelState.AddModelError("", "Confira os dados e tente novamente"); }
            }
            return View(cliente);
        }

        private string gerarSenha(Cliente cliente)
        {
            string senha = Convert.ToString(cliente.Data_nascimento.Day.ToString().PadLeft(2, '0'));
            senha += Convert.ToString(cliente.Data_nascimento.Month.ToString().PadLeft(2, '0'));
            senha += Convert.ToString(cliente.Data_nascimento.Year);

            return CriptoHelper.HashMD5(senha);
        }

        // GET: Cliente/Edit/5
        [Authorize(Roles = "Administrador, Funcionario")]
        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = await db.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Cliente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar([Bind(Include = "Id,Nome,Data_nascimento,Celular,Telefone,Email")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(cliente).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception e) { ModelState.AddModelError("", "Confira os dados e tente novamente"); }
            }
            return View(cliente);
        }

        // GET: Cliente/Delete/5
        [Authorize(Roles = "Administrador, Funcionario")]
        public async Task<ActionResult> Deletar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = await db.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Cliente cliente = await db.Clientes.FindAsync(id);
                db.Clientes.Remove(cliente);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception e) { ModelState.AddModelError("", "Confira os dados e tente novamente"); }
            return RedirectToAction("Deletar");
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
        public ActionResult GetClientes(string term)
        {
            var clientes = db.Clientes.ToList();

            if (term != null)
            {
                clientes = db.Clientes.Where(s => s.Nome.ToLower().StartsWith(term.ToLower())).ToList();
            }

            var Results = clientes.Select(s => new
            {
                text = s.Nome,
                id = s.Id

            });
            return Content(JsonConvert.SerializeObject(new { items = Results }));
            //return Json(Results, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetClientesDataTable()
        {
            var clientes = db.Clientes.ToList();

            return Content(JsonConvert.SerializeObject(new { data = clientes }));
        }

        public ActionResult ExportExcel()
        {
            var list = db.Clientes.ToList();
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
