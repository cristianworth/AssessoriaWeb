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
    public class AtletasController : Controller
    {
        private AssessoriaWebContext db = new AssessoriaWebContext();

        // GET: Atletas
        public ActionResult Index()
        {
            var atletas = db.Atletas.Include(a => a.Pessoa);
            return View(atletas.ToList());
        }

        // GET: Atletas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atleta atleta = db.Atletas.Find(id);
            db.Entry(atleta).Reference(p => p.Pessoa).Load();
            if (atleta == null)
            {
                return HttpNotFound();
            }
            return View(atleta);
        }

        // GET: Atletas/Create
        public ActionResult Create()
        {
            ViewBag.pes_id = new SelectList(db.Pessoas, "pes_id", "pes_nome");
            return View();
        }

        // POST: Atletas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "atl_id,atl_categoria,atl_observacao,pes_id")] Atleta atleta)
        {
            if (ModelState.IsValid)
            {
                db.Atletas.Add(atleta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.pes_id = new SelectList(db.Pessoas, "pes_id", "pes_nome", atleta.pes_id);
            return View(atleta);
        }

        // GET: Atletas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atleta atleta = db.Atletas.Find(id);
            if (atleta == null)
            {
                return HttpNotFound();
            }
            ViewBag.pes_id = new SelectList(db.Pessoas, "pes_id", "pes_nome", atleta.pes_id);
            return View(atleta);
        }

        // POST: Atletas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "atl_id,atl_categoria,atl_observacao,pes_id")] Atleta atleta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(atleta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pes_id = new SelectList(db.Pessoas, "pes_id", "pes_nome", atleta.pes_id);
            return View(atleta);
        }

        // GET: Atletas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atleta atleta = db.Atletas.Find(id);
            if (atleta == null)
            {
                return HttpNotFound();
            }
            return View(atleta);
        }

        // POST: Atletas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Atleta atleta = db.Atletas.Find(id);
            db.Atletas.Remove(atleta);
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
