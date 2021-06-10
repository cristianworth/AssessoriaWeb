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
    public class AssessorsController : Controller
    {
        private AssessoriaWebContext db = new AssessoriaWebContext();

        // GET: Assessors
        public ActionResult Index()
        {
            var assessors = db.Assessors.Include(a => a.Pessoa);
            return View(assessors.ToList());
        }

        // GET: Assessors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assessor assessor = db.Assessors.Find(id);
            db.Entry(assessor).Reference(p => p.Pessoa).Load();
            if (assessor == null)
            {
                return HttpNotFound();
            }
            return View(assessor);
        }

        // GET: Assessors/Create
        public ActionResult Create()
        {
            ViewBag.pes_id = new SelectList(db.Pessoas, "pes_id", "pes_nome");
            return View();
        }

        // POST: Assessors/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ass_id,ass_tipo,pes_id")] Assessor assessor)
        {
            if (ModelState.IsValid)
            {
                db.Assessors.Add(assessor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.pes_id = new SelectList(db.Pessoas, "pes_id", "pes_nome", assessor.pes_id);
            return View(assessor);
        }

        // GET: Assessors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assessor assessor = db.Assessors.Find(id);
            if (assessor == null)
            {
                return HttpNotFound();
            }
            ViewBag.pes_id = new SelectList(db.Pessoas, "pes_id", "pes_nome", assessor.pes_id);
            return View(assessor);
        }

        // POST: Assessors/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ass_id,ass_tipo,pes_id")] Assessor assessor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assessor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pes_id = new SelectList(db.Pessoas, "pes_id", "pes_nome", assessor.pes_id);
            return View(assessor);
        }

        // GET: Assessors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assessor assessor = db.Assessors.Find(id);
            db.Entry(assessor).Reference(p => p.Pessoa).Load();
            if (assessor == null)
            {
                return HttpNotFound();
            }
            return View(assessor);
        }

        // POST: Assessors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Assessor assessor = db.Assessors.Find(id);
            db.Assessors.Remove(assessor);
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
