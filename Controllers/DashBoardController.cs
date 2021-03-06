using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using AssessoriaWeb.Models;


namespace AssessoriaWeb.Controllers
{
    [Authorize(Roles = "admin,assessor,atleta")]
    public class DashBoardController : Controller
    {
        private AssessoriaWebContext db = new AssessoriaWebContext();
        public ActionResult Index()
        {
            var cultureInfo = new System.Globalization.CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            int pes_id = Convert.ToInt32(Session["pes_id"]);
            TimeSpan hora = DateTime.Now.TimeOfDay.Subtract(TimeSpan.FromHours(1));

            PlanoAlimentar plano = db.PlanoAlimentars.Where(pln => pln.Atleta.pes_id == pes_id && DateTime.Today >= pln.pln_datainicial && DateTime.Today <= pln.pln_datafinal).FirstOrDefault();
            Avaliacao avaliacao = db.Avaliacaos.Where(ava => ava.Atleta.pes_id == pes_id).OrderByDescending(ava => ava.ava_data).FirstOrDefault();
            Treinamento treinamento = db.Treinamentoes.Where(tre => tre.Atleta.pes_id == pes_id && tre.tre_data > DateTime.Today || ( tre.tre_hora >= hora && tre.tre_data == DateTime.Today)).Include(tre => tre.Assessor.Pessoa).OrderBy(tre => tre.tre_data).OrderBy(tre => tre.tre_hora).FirstOrDefault();
            ViewBag.treinamento = treinamento;
            ViewBag.plano = plano;
            ViewBag.avaliacao = avaliacao;

            return View();
        }

        public ActionResult Schedule()
        {
            return View();
        }
        public ActionResult ScheduleTable(DateTime date)
        {
            var cultureInfo = new System.Globalization.CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            DateTime startOfWeek = date.AddDays(-1 * (int)(date.DayOfWeek));
            DateTime endOfWeek = startOfWeek.AddDays(6);
            int pes_id = Convert.ToInt32(Session["pes_id"]);
            IList<Treinamento> lista = db.Treinamentoes.Where(tre => tre.Atleta.pes_id == pes_id && tre.tre_data >= startOfWeek && tre.tre_data <= endOfWeek).Include(tre => tre.Assessor.Pessoa).ToList();
            ViewBag.Date = date;
            int today = 0;
            if (startOfWeek <= DateTime.Today && DateTime.Today <= endOfWeek)
            {
                today = (int)DateTime.Today.DayOfWeek + 1;
            }
            ViewBag.today = today;
            ViewBag.startOfWeek = startOfWeek.ToString("dd/MM");
            ViewBag.endOfWeek = endOfWeek.ToString("dd/MM");
            ViewBag.lista_domingo = lista.Where(t => t.tre_data.DayOfWeek == DayOfWeek.Sunday).ToList();
            ViewBag.lista_segunda = lista.Where(t => t.tre_data.DayOfWeek == DayOfWeek.Monday).ToList();
            ViewBag.lista_terca = lista.Where(t => t.tre_data.DayOfWeek == DayOfWeek.Tuesday).ToList();
            ViewBag.lista_quarta = lista.Where(t => t.tre_data.DayOfWeek == DayOfWeek.Wednesday).ToList();
            ViewBag.lista_quinta = lista.Where(t => t.tre_data.DayOfWeek == DayOfWeek.Thursday).ToList();
            ViewBag.lista_sexta = lista.Where(t => t.tre_data.DayOfWeek == DayOfWeek.Friday).ToList();
            ViewBag.lista_sabado = lista.Where(t => t.tre_data.DayOfWeek == DayOfWeek.Saturday).ToList();
            ViewBag.horaInicial = TimeSpan.FromHours(6).Hours;
            ViewBag.horaFinal = TimeSpan.FromHours(23).Hours;

            return PartialView("ScheduleTable", date);
        }

        public ContentResult getInfoTreinamento(int? id)
        {
            Treinamento Treinamento = db.Treinamentoes.Where(tre => tre.tre_id == id).Include(tre => tre.Assessor.Pessoa).SingleOrDefault();
            IList<Atividade> list_treinamento = db.Database.SqlQuery<Atividade>(@"select Atividade.* from Treinamento
                inner join AtividadeTreinamento using(tre_id)
                inner join Atividade using(ati_id)
                where Treinamento.tre_id = @p0", id).ToList<Atividade>();
            string retorno = list_treinamento.Aggregate("", (previous, current) => previous + $"<h3>{current.ati_descricao}</h3><li>{current.atl_observacao}</li>");
            return new ContentResult
            {
                ContentType = "text/html",
                Content = $@"<div><div>
                                    <span><b>Assessor: </b><span>
                                    <h4 style=""display: inline-block"">{Treinamento.Assessor.Pessoa.pes_nome}</h4><div>
                                    <span style=""margin-right:20px""><b>Fone: </b>{Treinamento.Assessor.Pessoa.pes_telefone}</span>
                                    <span><b>E-Mail: </b>{Treinamento.Assessor.Pessoa.pes_email}</span>
                                    <hr>{retorno}
                                </div>"
            };
        }
    }
}