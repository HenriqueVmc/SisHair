using SisHair.Presentation.Web.MVC.Old.Context;
using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using SisHair.Presentation.Web.MVC.Old.Models;

namespace SisHair.Presentation.Web.MVC.Old.Controllers
{
    public class CaixaController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Caixa
        public async Task<ActionResult> Index()
        {
            var caixa = db.Caixa.Include(c => c.Agendamento);
            return View(await caixa.ToListAsync());
        }

        // GET: Caixa/Create
        [Authorize(Roles = "Administrador, Funcionario")]
        public ActionResult Pagamento(int? id)
        {
            try
            {
                if (id > 0)
                {
                    /*            
                    SELECT a.Descricao, SUM(s.Valor) AS 'Valor Total' FROM ServicosAgendamentoes sa
                    LEFT JOIN Agendamentoes a ON sa.AgendamentoId = a.Id
                    JOIN Servicoes s ON sa.ServicoId = s.Id GROUP BY a.Descricao
                    */
                    ViewBag.ValorTotal = db.ServicosAgendamento.Where(sa => sa.AgendamentoId == id).Include(sa => sa.Servico).Select(sa => new
                    {
                        sa.Servico.Valor
                    }).Sum(s => s.Valor);

                    ViewBag.Agendamento = db.Agendamentos.Where(a => a.Id == id).SingleOrDefault();

                    return View();
                }
            }
            catch (Exception e) { ModelState.AddModelError("", "Algo deu errado... tente novamente mais tarde."); }
            return RedirectToAction("Index");
        }

        // POST: Caixa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Pagamento([Bind(Include = "Id,ValorTotal,ValorPago,Divida,FormaPagamento,DataPagamento,Status,AgendamentoId")] Caixa caixa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Caixa.Add(caixa);

                    //Alterar Situação do Agendamento; Ou, Excluir e add no Histórico?            
                    Agendamento agendamento = db.Agendamentos.Where(a => a.Id == caixa.AgendamentoId).SingleOrDefault();
                    agendamento.Situacao = "Pago";

                    //db.Agendamentos.Remove(agendamento);

                    db.SaveChanges();

                    //db.Solicitacoes.Where(s => s.DataHoraInicio == agendamento.DataHoraInicio &&
                    //                           s.DataHoraFinal == agendamento.DataHoraFinal &&
                    //                           s.ClienteId == agendamento.ClienteId &&
                    //                           s.FuncionarioId == agendamento.FuncionarioId).SingleOrDefault();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception e) { ModelState.AddModelError("", "Algo deu errado... tente novamente mais tarde."); }

            ViewBag.ValorTotal = db.ServicosAgendamento.Where(sa => sa.AgendamentoId == caixa.AgendamentoId).Include(sa => sa.Servico).Select(sa => new
            {
                sa.Servico.Valor
            }).Sum(s => s.Valor);

            ViewBag.Agendamento = db.Agendamentos.Where(a => a.Id == caixa.AgendamentoId).SingleOrDefault();

            return View(caixa);
        }

        // GET: Caixa/Edit/5
        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caixa caixa = await db.Caixa.FindAsync(id);
            if (caixa == null)
            {
                return HttpNotFound();
            }

            ViewBag.Agendamento = db.Agendamentos.Where(a => a.Id == caixa.AgendamentoId).SingleOrDefault();
            ViewBag.ValorTotal = db.Caixa.Where(c => c.Id == id).Select(c => new
            {
                c.ValorTotal
            }).SingleOrDefault();

            return View(caixa);
        }

        // POST: Caixa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Editar([Bind(Include = "Id,ValorTotal,ValorPago,Divida,FormaPagamento,DataPagamento,Status,AgendamentoId")] Caixa caixa)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(caixa).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e) { ModelState.AddModelError("", "Algo deu errado... tente novamente mais tarde."); }
            }
            ViewBag.ValorTotal = db.ServicosAgendamento.Where(sa => sa.AgendamentoId == caixa.AgendamentoId).Include(sa => sa.Servico).Select(sa => new
            {
                sa.Servico.Valor
            }).Sum(s => s.Valor);

            ViewBag.Agendamento = db.Agendamentos.Where(a => a.Id == caixa.AgendamentoId).SingleOrDefault();
            return View(caixa);
        }

        // GET: Caixa/Delete/5
        public async Task<ActionResult> Deletar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caixa caixa = await db.Caixa.FindAsync(id);
            if (caixa == null)
            {
                return HttpNotFound();
            }

            return View(caixa);
        }

        // POST: Caixa/Delete/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Caixa caixa = await db.Caixa.FindAsync(id);
                db.Caixa.Remove(caixa);
                await db.SaveChangesAsync();
            }
            catch (Exception e) { ModelState.AddModelError("", "Algo deu errado... tente novamente mais tarde."); }
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
            var list = db.Caixa.ToList();
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
