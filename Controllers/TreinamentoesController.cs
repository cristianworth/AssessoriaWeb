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
    public class TreinamentoesController : Controller
    {
        private AssessoriaWebContext db = new AssessoriaWebContext();

        // GET: Treinamentoes
        public ActionResult Index()
        {
            var treinamentoes = db.Treinamentoes.Include(t => t.Assessor).Include(t => t.Atleta);
            return View(treinamentoes.ToList());
        }

        // GET: Treinamentoes/Details/5
        public ActionResult Details(int? id)
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
            return View(treinamento);
        }

        // GET: Treinamentoes/Create
        public ActionResult Create()
        {
            ViewBag.ass_id = new SelectList(db.Assessors, "ass_id", "ass_tipo");
            ViewBag.atl_id = new SelectList(db.Atletas, "atl_id", "atl_categoria");
            return View();
        }

        // POST: Treinamentoes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tre_id,tre_data,tre_hora,tre_valor,ass_id,atl_id")] Treinamento treinamento)
        {
            if (ModelState.IsValid)
            {
                db.Treinamentoes.Add(treinamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ass_id = new SelectList(db.Assessors, "ass_id", "ass_tipo", treinamento.ass_id);
            ViewBag.atl_id = new SelectList(db.Atletas, "atl_id", "atl_categoria", treinamento.atl_id);
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
            ViewBag.ass_id = new SelectList(db.Assessors, "ass_id", "ass_tipo", treinamento.ass_id);
            ViewBag.atl_id = new SelectList(db.Atletas, "atl_id", "atl_categoria", treinamento.atl_id);
            return View(treinamento);
        }

        // POST: Treinamentoes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tre_id,tre_data,tre_hora,tre_valor,ass_id,atl_id")] Treinamento treinamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(treinamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ass_id = new SelectList(db.Assessors, "ass_id", "ass_tipo", treinamento.ass_id);
            ViewBag.atl_id = new SelectList(db.Atletas, "atl_id", "atl_categoria", treinamento.atl_id);
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
    }
}
