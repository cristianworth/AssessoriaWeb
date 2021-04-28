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
    [Authorize]
    public class AvaliacaosController : Controller
    {
        private AssessoriaWebContext db = new AssessoriaWebContext();

        // GET: Avaliacaos
        public ActionResult Index()
        {
            var avaliacaos = db.Avaliacaos.Include(p => p.Atleta).Include(a => a.Atleta.Pessoa);
            return View(avaliacaos.ToList());
        }

        // GET: Avaliacaos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avaliacao avaliacao = db.Avaliacaos.Find(id);
            db.Entry(avaliacao).Reference(p => p.Atleta).Load();
            db.Entry(avaliacao.Atleta).Reference(p => p.Pessoa).Load();
            if (avaliacao == null)
            {
                return HttpNotFound();
            }
            return View(avaliacao);
        }

        // GET: Avaliacaos/Create
        public ActionResult Create()
        {
            ViewBag.atl_id = new SelectList(db.Atletas.Include(a => a.Pessoa), "atl_id", "Pessoa.pes_nome");
            return View();
        }

        // POST: Avaliacaos/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ava_id,ava_data,ava_peso,ava_gorduraVisceral,ava_gorduraCorporal,ava_bioimpedancia,atl_id")] Avaliacao avaliacao)
        {
            if (ModelState.IsValid)
            {
                db.Avaliacaos.Add(avaliacao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.atl_id = new SelectList(db.Atletas.Include(a => a.Pessoa), "atl_id", "Pessoa.pes_nome", avaliacao.atl_id);
            return View(avaliacao);
        }

        // GET: Avaliacaos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avaliacao avaliacao = db.Avaliacaos.Find(id);
            if (avaliacao == null)
            {
                return HttpNotFound();
            }
            ViewBag.atl_id = new SelectList(db.Atletas.Include(a => a.Pessoa), "atl_id", "Pessoa.pes_nome", avaliacao.atl_id);
            return View(avaliacao);
        }

        // POST: Avaliacaos/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ava_id,ava_data,ava_peso,ava_gorduraVisceral,ava_gorduraCorporal,ava_bioimpedancia,atl_id")] Avaliacao avaliacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(avaliacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.atl_id = new SelectList(db.Atletas.Include(a => a.Pessoa), "atl_id", "Pessoa.pes_nome", avaliacao.atl_id);
            return View(avaliacao);
        }

        // GET: Avaliacaos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avaliacao avaliacao = db.Avaliacaos.Find(id);
            if (avaliacao == null)
            {
                return HttpNotFound();
            }
            return View(avaliacao);
        }

        // POST: Avaliacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Avaliacao avaliacao = db.Avaliacaos.Find(id);
            db.Avaliacaos.Remove(avaliacao);
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
    }
}
