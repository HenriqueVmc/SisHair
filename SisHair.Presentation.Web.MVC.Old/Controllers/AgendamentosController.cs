using SisHair.Presentation.Web.MVC.Old.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using SisHair.Presentation.Web.MVC.Old.Models;

namespace SisHair.Presentation.Web.MVC.Old.Controllers
{
    public class AgendamentosController : Controller
    {
        private DBContext db = new DBContext();
        // GET: Agendamentos

        [Authorize(Roles = "Administrador, Funcionario")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAgendamentos()//int? id
        {
            int id = (int)Session["FuncionarioId"];
            var agendamentos = db.Agendamentos.Select(a => new { a.Id, a.DataHoraInicio, a.DataHoraFinal, a.FuncionarioId, Funcionario = a.Funcionario.Nome, a.ClienteId, Cliente = a.Cliente.Nome, a.Situacao, a.Descricao, a.Servicos }).ToList();//.Where(a => a.FuncionarioId == id)

            if (id > 0)
            {
                agendamentos = null;
                agendamentos = db.Agendamentos.Where(a => a.FuncionarioId == id).Select(a => new { a.Id, a.DataHoraInicio, a.DataHoraFinal, a.FuncionarioId, Funcionario = a.Funcionario.Nome, a.ClienteId, Cliente = a.Cliente.Nome, a.Situacao, a.Descricao, a.Servicos }).ToList();//.Where(a => a.FuncionarioId == id)                
            }

            return new JsonResult { Data = agendamentos, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult Salvar([Bind(Include = "Id,DataHoraInicio,DataHoraFinal,Descricao,Situacao,ClienteId,FuncionarioId")]Agendamento a, List<int> servicos)
        {
            var status = false;
            try
            {
                if (a.Id > 0)
                {
                    //Update no Agendamento
                    var agendamento = db.Agendamentos.Where(agen => agen.Id == a.Id).SingleOrDefault();

                    if (agendamento != null)
                    {
                        agendamento.DataHoraInicio = a.DataHoraInicio;
                        agendamento.DataHoraFinal = a.DataHoraFinal;
                        agendamento.Situacao = a.Situacao;
                        agendamento.FuncionarioId = a.FuncionarioId;
                        agendamento.ClienteId = a.ClienteId;
                        agendamento.Descricao = a.Descricao;

                        db.Entry(agendamento).State = EntityState.Modified;

                        if (servicos != null)
                        {
                            var servicosAgendamento = db.ServicosAgendamento.Where(sa => sa.AgendamentoId == a.Id).ToList();
                            if (servicosAgendamento != null)
                            {
                                foreach (ServicosAgendamento servicoA in servicosAgendamento)
                                {
                                    db.ServicosAgendamento.Remove(servicoA);
                                }
                            }

                            agendamento.Servicos = null;
                            agendamento.Servicos = new ServicosAgendamento().salvarServicosAgendamento(agendamento, servicos);
                        }
                    }               
                }
                else
                {
                    db.Agendamentos.Add(a);

                    if (servicos != null)
                    {
                        foreach (int idServico in servicos)
                        {
                            var servico = db.Servicos.Where(s => s.Id == idServico).SingleOrDefault();

                            ServicosAgendamento sa = new ServicosAgendamento();
                            sa.AgendamentoId = a.Id;
                            sa.ServicoId = servico.Id;

                            db.ServicosAgendamento.Add(sa);

                            a.Servicos += (string.IsNullOrEmpty(a.Servicos)) ? sa.Servico.Nome : ", " + sa.Servico.Nome;
                        }
                    }
                }
                db.SaveChanges();
                status = true;
            }
            catch (Exception e) { }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult Deletar(int id)
        {
            var status = false;

            try
            {
                var result = db.Agendamentos.Where(agen => agen.Id == id).FirstOrDefault();
                if (result != null)
                {
                    db.Agendamentos.Remove(result);
                    db.SaveChanges();
                    status = true;
                }
            }
            catch (Exception e) { }
            return new JsonResult { Data = new { status = status } };
        }

        public ActionResult Solicitacoes()
        {
            var solicitacoes = db.Solicitacoes.Where(a=> a.Situacao.Contains("Pendente")).ToList();
            return View(solicitacoes);
        }

        [HttpGet]
        public ActionResult SalvarSolicitacao(int id)
        {
            try
            {
                var s = db.Solicitacoes.Where(soli => soli.Id == id).SingleOrDefault();

                var agendamento = new Agendamento()
                {
                    DataHoraInicio = s.DataHoraInicio,
                    DataHoraFinal = s.DataHoraFinal,
                    FuncionarioId = s.FuncionarioId,
                    ClienteId = s.ClienteId,
                    Situacao = "Confirmado",
                    Descricao = s.Descricao,
                    Servicos = s.Servicos
                };

                db.Agendamentos.Add(agendamento);
                s.Situacao = "Confirmado";

                EnviarEmail(s);

                db.Entry(s).State = EntityState.Modified;
                db.SaveChanges();

                var servicosSolicitacao = db.ServicosSolicitacao.Where(ss => ss.SolicitacaoId == id).Select(ss => new { ss.ServicoId }).ToList();

                if (servicosSolicitacao != null)
                {
                    foreach (var ss in servicosSolicitacao)
                    {
                        var servico = db.Servicos.Where(ser => ser.Id == ss.ServicoId).SingleOrDefault();

                        ServicosAgendamento sa = new ServicosAgendamento();
                        sa.AgendamentoId = agendamento.Id;
                        sa.ServicoId = servico.Id;

                        db.ServicosAgendamento.Add(sa);
                        db.SaveChanges();
                    }
                }       

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Algo deu errado");
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult CancelarSolicitacao(int id)
        {
            try
            {
                var s = db.Solicitacoes.Where(soli => soli.Id == id).SingleOrDefault();

                db.Solicitacoes.Remove(s);
                db.SaveChanges();
            }catch(Exception e) { ModelState.AddModelError("", "Algo deu errado"); }

            return RedirectToAction("Solicitacoes");
        }

        public void EnviarEmail(Solicitacao s)
        {
            try
            {
                var ss = db.Clientes.Where(a => a.Id == s.ClienteId).SingleOrDefault();
                string assunto = @"Olá, sua solitação foi confirmada. Obrigado!";

                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("salaosuporte@gmail.com");
                mail.To.Add(ss.Email);
                mail.Subject = assunto;

                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("salaosuporte@gmail.com", "suporteadm");
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            catch (Exception ex){ }
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
    }

}