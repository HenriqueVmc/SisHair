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
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

namespace TrabalhoTcc.Controllers
{
    public class CargosController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Cargo
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Index()
        {
            return View(await db.Cargos.ToListAsync());
        }

        // GET: Cargo/Details/5
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargo cargo = await db.Cargos.FindAsync(id);
            if (cargo == null)
            {
                return HttpNotFound();
            }
            return View(cargo);
        }

        // GET: Cargo/Cadastrar
        [Authorize(Roles = "Administrador")]
        public ActionResult Cadastrar()
        {
            return View();
        }

        // POST: Cargo/Cadastrar
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Cadastrar([Bind(Include = "Id,Nome,Descricao")] Cargo cargo)
        {
            if (ModelState.IsValid)
            {
                cargo.RegistroCargoAtivo = true;
                db.Cargos.Add(cargo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cargo);
        }

        // GET: Cargo/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargo cargo = await db.Cargos.FindAsync(id);
            if (cargo == null)
            {
                return HttpNotFound();
            }
            return View(cargo);
        }

        // POST: Cargo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Editar([Bind(Include = "Id,Nome,Descricao")] Cargo cargo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cargo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cargo);
        }

        // GET: Cargo/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Deletar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargo cargo = await db.Cargos.FindAsync(id);
            if (cargo == null)
            {
                return HttpNotFound();
            }
            return View(cargo);
        }

        // POST: Cargo/Delete/5
         [Authorize(Roles = "Administrador")]
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Cargo cargo = await db.Cargos.FindAsync(id);
            db.Cargos.Remove(cargo);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

         [Authorize(Roles = "Administrador")]
         [HttpPost, ActionName("InativarRegistro")]
         [ValidateAntiForgeryToken]
         public ActionResult InativarRegistro(int id)
         {
             Cargo cargo = db.Cargos.Find(id);
             cargo.RegistroCargoAtivo = false;
             db.Entry(cargo).State = EntityState.Modified;
             db.SaveChanges();             
             return RedirectToAction("Index");
         }

        [Authorize(Roles = "Administrador")]
        [HttpPost, ActionName("AtivarRegistro")]
        public ActionResult AtivarRegistro(int? id)
        {
            Cargo cargo = db.Cargos.Find(id);
            cargo.RegistroCargoAtivo = true;            
            db.Entry(cargo).State = EntityState.Modified;
            db.SaveChanges();            
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

        public ActionResult ExportExcel()
        {
            var list = db.Cargos.ToList();
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
