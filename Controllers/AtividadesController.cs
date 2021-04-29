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
    public class AtividadesController : Controller
    {
        private AssessoriaWebContext db = new AssessoriaWebContext();

        // GET: Atividades
        public ActionResult Index()
        {
            return View(db.Atividades.ToList());
        }

        // GET: Atividades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atividade atividade = db.Atividades.Find(id);
            if (atividade == null)
            {
                return HttpNotFound();
            }
            return View(atividade);
        }

        // GET: Atividades/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Atividades/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ati_id,ati_descricao,atl_observacao")] Atividade atividade)
        {
            if (ModelState.IsValid)
            {
                db.Atividades.Add(atividade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(atividade);
        }

        // GET: Atividades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atividade atividade = db.Atividades.Find(id);
            if (atividade == null)
            {
                return HttpNotFound();
            }
            return View(atividade);
        }

        // POST: Atividades/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ati_id,ati_descricao,atl_observacao")] Atividade atividade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(atividade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(atividade);
        }

        // GET: Atividades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atividade atividade = db.Atividades.Find(id);
            if (atividade == null)
            {
                return HttpNotFound();
            }
            return View(atividade);
        }

        // POST: Atividades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Atividade atividade = db.Atividades.Find(id);
            db.Atividades.Remove(atividade);
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
