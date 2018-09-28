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
using Newtonsoft.Json;
using TrabalhoTcc.Models.Conta;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Net.Mail;

namespace TrabalhoTcc.Controllers
{
    public class SolicitacaoController : Controller
    {
        private DBContext db = new DBContext();

        [HttpGet]
        public ActionResult Agendamento()
        {
            try
            {
                int id = (int)Session["ClienteId"];
                var j = db.Clientes.Where(a => a.Id == id).SingleOrDefault();
                var upper = char.ToUpper(j.Nome[0]) + j.Nome.Substring(1);
                string Nome = upper;
                if (id > 0)
                {
                    //Receber cliente e rotornar em ViewBag para campos hiddens 
                    ViewBag.ClienteId = id;
                    ViewBag.Nome = Nome;
                    ViewBag.Funcionarios = db.Funcionarios.Include(f => f.Cargo).Where(a => a.RegistroFuncionarioAtivo == true).ToList();
                    return View();
                }
            }
            catch (Exception e)
            {
                return Redirect("/ContaCliente/Login");
            }

            return Redirect("/ContaCliente/Login");
        }

        [HttpGet]
        public ActionResult AgendamentoModal(int? id)
        {
            ViewBag.FuncionarioId = id;
            return View();
        }

        [HttpPost]
        public ActionResult Agendamento([Bind(Include = "Id, DataHoraInicio, DataHoraFinal, ClienteId, FuncionarioId, Descricao")]Solicitacao solicitacao, List<int> servicos)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    solicitacao.Situacao = "Pendente";
                    db.Solicitacoes.Add(solicitacao);

                    if (servicos != null)
                    {
                        foreach (int idServico in servicos)
                        {
                            var servico = db.Servicos.Where(s => s.Id == idServico).SingleOrDefault();

                            ServicosSolicitacao sa = new ServicosSolicitacao();
                            sa.SolicitacaoId = solicitacao.Id;
                            sa.ServicoId = servico.Id;
                            solicitacao.Servicos += (string.IsNullOrEmpty(solicitacao.Servicos)) ? servico.Nome : ", " + servico.Nome;

                            db.ServicosSolicitacao.Add(sa);
                            db.SaveChanges();
                        }

                        EnviarEmail(solicitacao);
                    }

                    return Content(JsonConvert.SerializeObject(new { id = solicitacao.Id }));
                }
                catch (Exception e) { ModelState.AddModelError("", "Confira os dados e tente novamente"); }
            }

            ViewBag.ClienteId = solicitacao.ClienteId;
            ViewBag.Funcionarios = db.Funcionarios.Include(f => f.Cargo).ToList();

            return View(solicitacao);
        }

        public ActionResult Cancelar(int? id)
        {
            try
            {
                Solicitacao solicitacao = db.Solicitacoes.Find(id);
                db.Solicitacoes.Remove(solicitacao);
                db.SaveChanges();
            }
            catch (Exception e) { ModelState.AddModelError("", "Confira os dados e tente novamente"); }
            return RedirectToAction("Index");
        }

        // GET: Solicitacao
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                int id = (int)Session["ClienteId"];
                if (id > 0)
                {
                    var solicitacoes = db.Solicitacoes.Where(s => s.ClienteId == id).Include(s => s.Cliente).Include(s => s.Funcionario).ToList();
                    return View(solicitacoes);
                }
            }
            catch (Exception e)
            {
                return Redirect("/ContaCliente/Login");
            }

            return Redirect("/ContaCliente/Login");
        }

        // GET: Solicitacao/Edit/5
        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitacao solicitacao = await db.Solicitacoes.FindAsync(id);
            if (solicitacao == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nome", solicitacao.ClienteId);
            ViewBag.FuncionarioId = new SelectList(db.Funcionarios, "Id", "Nome", solicitacao.FuncionarioId);
            return View(solicitacao);
        }

        // POST: Solicitacao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar([Bind(Include = "Id,DataHoraInicio,DataHoraFinal,Descricao,FuncionarioId,ClienteId")] Solicitacao solicitacao)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(solicitacao).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
                catch (Exception e) { ModelState.AddModelError("", "Confira os dados e tente novamente"); }
                return RedirectToAction("Index");
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nome", solicitacao.ClienteId);
            ViewBag.FuncionarioId = new SelectList(db.Funcionarios, "Id", "Nome", solicitacao.FuncionarioId);
            return View(solicitacao);
        }

        // GET: Solicitacao/Delete/5
        public async Task<ActionResult> Deletar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitacao solicitacao = await db.Solicitacoes.FindAsync(id);
            if (solicitacao == null)
            {
                return HttpNotFound();
            }
            return View(solicitacao);
        }

        // POST: Solicitacao/Delete/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Solicitacao solicitacao = await db.Solicitacoes.FindAsync(id);
            db.Solicitacoes.Remove(solicitacao);
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
        public ActionResult AreaDoCliente()
        {
            try
            {
                int id = (int)Session["ClienteId"];
                if (id > 0)
                {
                    //Receber cliente e rotornar em ViewBag para campos hiddens 
                    ViewBag.ClienteId = id;
                    ViewBag.AgendamentosParaAvaliar = db.Agendamentos.Where(c => c.ClienteId == id).ToList();
                    return View();
                    // - bool para bloquear realizar a avaliacao mais de uma vez, if avaliou == false { permitir avaliar }
                }
            }
            catch (Exception e)
            {
                return Redirect("/ContaCliente/Login");
            }

            return Redirect("/ContaCliente/Login");
        }

        [HttpGet]
        public ActionResult AvaliacaoModal(int? id)
        {
            ViewBag.AgendamentoId = id;
            return View();
        }

        [HttpPost]
        public ActionResult SalvarAvaliacao(Avaliacao avaliacao)
        {
            avaliacao.AvaliouSalao = true;
            if (ModelState.IsValid)
            {
                db.Avaliacoes.Add(avaliacao);
                db.SaveChanges();
                return Content(JsonConvert.SerializeObject(new { id = avaliacao.Id }));
            }

            //ViewBag.ClienteId = avaliacao.Agendamento.ClienteId;
            // ViewBag.Avaliacoes = db.Funcionarios.Include(f => f.Cargo).ToList();

            return View(avaliacao);
        }

        public ActionResult MinhasAvaliacoes()
        {
            try
            {
                int id = (int)Session["ClienteId"];
                if (id > 0)
                {
                    //Receber cliente e rotornar em ViewBag para campos hiddens 
                    ViewBag.ClienteId = id;
                    //ViewBag.AgendamentosParaAvaliar = db.Agendamentos.Where(c => c.ClienteId == id).ToList();
                    ViewBag.MinhasAvaliacoes = db.Avaliacoes.Where(c => c.Id == id).ToList();
                    return View();
                }
            }
            catch (NullReferenceException)
            {
                return Redirect("/ContaCliente/Login");
            }

            return Redirect("/ContaCliente/Login");

        }

        public ActionResult ExportExcel()
        {
            var list = db.Solicitacoes.ToList();
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

        public void EnviarEmail(Solicitacao s)
        {
            try
            {
                int a = s.FuncionarioId;
                var aa = db.Funcionarios.Where(aaa => aaa.Id == a).SingleOrDefault();
                var callbackUrl = Url.Action("Solicitacoes", "Agendamentos", null, protocol: Request.Url.Scheme);

                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("salaosuporte@gmail.com");
                mail.To.Add(aa.Email);
                mail.Subject = "SisHAIR";
                mail.Body = string.Format("Uma solicitação de agendamento foi realizada! Consulte sua <a href='{0}'>Lista de Solicitações</a> ", callbackUrl);

                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("salaosuporte@gmail.com", "suporteadm");
                smtp.EnableSsl = true;
                smtp.Send(mail);

            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}
