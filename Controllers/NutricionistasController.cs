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
    public class NutricionistasController : Controller
    {
        private AssessoriaWebContext db = new AssessoriaWebContext();

        // GET: Nutricionistas
        public ActionResult Index()
        {
            var nutricionistas = db.Nutricionistas.Include(n => n.Pessoa);
            return View(nutricionistas.ToList());
        }

        // GET: Nutricionistas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nutricionista nutricionista = db.Nutricionistas.Find(id);
            if (nutricionista == null)
            {
                return HttpNotFound();
            }
            return View(nutricionista);
        }

        // GET: Nutricionistas/Create
        public ActionResult Create()
        {
            ViewBag.pes_id = new SelectList(db.Pessoas, "pes_id", "pes_nome");
            return View();
        }

        // POST: Nutricionistas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nut_id,nut_crn,nut_cnpj,pes_id")] Nutricionista nutricionista)
        {
            if (ModelState.IsValid)
            {
                db.Nutricionistas.Add(nutricionista);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.pes_id = new SelectList(db.Pessoas, "pes_id", "pes_nome", nutricionista.pes_id);
            return View(nutricionista);
        }

        // GET: Nutricionistas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nutricionista nutricionista = db.Nutricionistas.Find(id);
            if (nutricionista == null)
            {
                return HttpNotFound();
            }
            ViewBag.pes_id = new SelectList(db.Pessoas, "pes_id", "pes_nome", nutricionista.pes_id);
            return View(nutricionista);
        }

        // POST: Nutricionistas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "nut_id,nut_crn,nut_cnpj,pes_id")] Nutricionista nutricionista)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nutricionista).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pes_id = new SelectList(db.Pessoas, "pes_id", "pes_nome", nutricionista.pes_id);
            return View(nutricionista);
        }

        // GET: Nutricionistas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nutricionista nutricionista = db.Nutricionistas.Find(id);
            if (nutricionista == null)
            {
                return HttpNotFound();
            }
            return View(nutricionista);
        }

        // POST: Nutricionistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nutricionista nutricionista = db.Nutricionistas.Find(id);
            db.Nutricionistas.Remove(nutricionista);
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
