using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssessoriaWeb.Models;

namespace AssessoriaWeb.Controllers
{
    [Authorize(Roles = "admin,assessor")]
    public class TreinamentoesController : Controller
    {
        private AssessoriaWebContext db = new AssessoriaWebContext();

        // GET: Treinamentoes
        public ActionResult Index()
        {
            var treinamentos = db.Treinamentoes.Include(p => p.Atleta).Include(a => a.Atleta.Pessoa);
            treinamentos = treinamentos.Include(t => t.Assessor).Include(t => t.Assessor.Pessoa);
            return View(treinamentos.ToList());
        }

        // GET: Treinamentoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Treinamento treinamento = db.Treinamentoes.Find(id);
            db.Entry(treinamento).Reference(p => p.Atleta).Load();
            db.Entry(treinamento.Atleta).Reference(p => p.Pessoa).Load();
            db.Entry(treinamento).Reference(p => p.Assessor).Load();
            db.Entry(treinamento.Assessor).Reference(p => p.Pessoa).Load();

            if (treinamento == null)
            {
                return HttpNotFound();
            }
            return View(treinamento);
        }

        // GET: Treinamentoes/Create
        public ActionResult Create()
        {
            ViewBag.ass_id = new SelectList(db.Assessors.Include(a => a.Pessoa), "ass_id", "Pessoa.pes_nome");
            ViewBag.atl_id = new SelectList(db.Atletas.Include(a => a.Pessoa), "atl_id", "Pessoa.pes_nome");
            ViewBag.atividades = new MultiSelectList(db.Atividades, "ati_id", "ati_descricao");

            return View();
        }

        // POST: Treinamentoes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tre_id,tre_data,tre_hora,tre_valor,tre_descricao,ass_id,atl_id")] Treinamento treinamento, int[] AtividadeTreinamentos)
        {

            foreach (int id in AtividadeTreinamentos)
            {
                treinamento.AtividadeTreinamentos.Add(new AtividadeTreinamento { ati_id = id });
            }

            if (ModelState.IsValid)
            {
                db.Treinamentoes.Add(treinamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ass_id = new SelectList(db.Assessors.Include(a => a.Pessoa), "ass_id", "Pessoa.pes_nome", treinamento.ass_id);
            ViewBag.atl_id = new SelectList(db.Atletas.Include(a => a.Pessoa), "atl_id", "Pessoa.pes_nome", treinamento.atl_id);
            ViewBag.atividades = new MultiSelectList(db.Atividades, "ati_id", "ati_descricao");
            return View(treinamento);
        }

        // GET: Treinamentoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treinamento treinamento = db.Treinamentoes.Find(id);
            if (treinamento == null)
            {
                return HttpNotFound();
            }

            ViewBag.ass_id = new SelectList(db.Assessors.Include(a => a.Pessoa), "ass_id", "Pessoa.pes_nome", treinamento.ass_id);
            ViewBag.atl_id = new SelectList(db.Atletas.Include(a => a.Pessoa), "atl_id", "Pessoa.pes_nome", treinamento.atl_id);
            ViewBag.atividades = new MultiSelectList(db.Atividades, "ati_id", "ati_descricao");
            ViewBag.atividadesSelecionadas = db.AtividadeTreinamentos.Where(x => x.tre_id == id).Select(x => x.ati_id).ToList<int>();
            return View(treinamento);
        }

        // POST: Treinamentoes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tre_id,tre_data,tre_hora,tre_valor,tre_descricao,ass_id,atl_id")] Treinamento treinamento, int[] AtividadeTreinamentos)
        {
            List<AtividadeTreinamento> atividadesRelacionadas = db.AtividadeTreinamentos.Where(x => x.tre_id == treinamento.tre_id).ToList();

            foreach (AtividadeTreinamento ati in atividadesRelacionadas)
            {
                db.AtividadeTreinamentos.Remove(ati); /*Remove todas as Atividades do Treinamento*/
                db.SaveChanges();
            }

            foreach (int id in AtividadeTreinamentos)
            {
                treinamento.AtividadeTreinamentos.Add(new AtividadeTreinamento { ati_id = id }); /*Adiciona as novas Atividades no Treinamento*/
            }

            if (ModelState.IsValid)
            {
                db.Treinamentoes.Add(treinamento);
                db.Entry(treinamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ass_id = new SelectList(db.Assessors.Include(a => a.Pessoa), "ass_id", "Pessoa.pes_nome", treinamento.ass_id);
            ViewBag.atl_id = new SelectList(db.Atletas.Include(a => a.Pessoa), "atl_id", "Pessoa.pes_nome", treinamento.atl_id);
            ViewBag.atividades = new MultiSelectList(db.Atividades, "ati_id", "ati_descricao");
            ViewBag.atividadesSelecionadas = db.AtividadeTreinamentos.Where(x => x.tre_id == treinamento.tre_id).Select(x => x.ati_id).ToList<int>();
            return View(treinamento);
        }

        // GET: Treinamentoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Treinamento treinamento = db.Treinamentoes.Find(id);
            db.Entry(treinamento).Reference(p => p.Atleta).Load();
            db.Entry(treinamento.Atleta).Reference(p => p.Pessoa).Load();
            db.Entry(treinamento).Reference(p => p.Assessor).Load();
            db.Entry(treinamento.Assessor).Reference(p => p.Pessoa).Load();
            
            if (treinamento == null)
            {
                return HttpNotFound();
            }
            return View(treinamento);
        }

        // POST: Treinamentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Treinamento treinamento = db.Treinamentoes.Find(id);
            db.Treinamentoes.Remove(treinamento);
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
        [HttpGet]
        public ActionResult ValidateDate(DateTime tre_data, TimeSpan? tre_hora, int atl_id = 0, int tre_id=0)
        {
            bool verifica = false;
            if (tre_hora == null)
            {
                tre_hora = DateTime.Now.TimeOfDay.Add(TimeSpan.FromHours(1));
            }
            else {
                verifica = atl_id != 0;
            }
            if (tre_data > DateTime.Now || (tre_data == DateTime.Now  && tre_hora > DateTime.Now.TimeOfDay ))
            {
                if (verifica) {
                    List<Treinamento> list = db.Treinamentoes.Where(tre => tre.atl_id == atl_id && tre.tre_id != tre_id).ToList();
                    int count = list.Where(tre => tre.tre_data == tre_data && !( tre_hora > tre.tre_hora.Add(TimeSpan.FromHours(1)) || tre_hora < tre.tre_hora.Subtract(TimeSpan.FromHours(1))) ).Count();
                    if (count > 0)
                    {
                        return Json("A atleja já possui um treinamento nesse horario", JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json("A data deve ser maior que a data atual", JsonRequestBehavior.AllowGet);
        }   
        [HttpGet]
        public ActionResult ValidateHour(TimeSpan tre_hora, DateTime? tre_data, int atl_id = 0, int tre_id = 0)
        {
            bool verifica = false;
            if (tre_data == null)
            {
                tre_data = DateTime.Now;
            }
            else
            {
                verifica = atl_id != 0;
            }
            TimeSpan dataInicial = TimeSpan.FromHours(6);
            TimeSpan dataFinal = TimeSpan.FromHours(22);
            if (tre_hora > dataInicial && tre_hora < dataFinal)
            {
                if (verifica)
                {
                    List<Treinamento> list = db.Treinamentoes.Where(tre => tre.atl_id == atl_id && tre.tre_id != tre_id).ToList();
                    int count = list.Where(tre => tre.tre_data == tre_data && !(tre_hora > tre.tre_hora.Add(TimeSpan.FromHours(1)) || tre_hora < tre.tre_hora.Subtract(TimeSpan.FromHours(1)))).Count();
                    if (count > 0)
                    {
                        return Json("A atleja já possui um treinamento nesse horario", JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json("A hora deve estar entre as 07:00 e as 22:00", JsonRequestBehavior.AllowGet);
        }    

    }
}
