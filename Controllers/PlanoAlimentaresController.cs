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
    [Authorize(Roles = "admin,nutricionista")]
    public class PlanoAlimentaresController : Controller
    {
        private AssessoriaWebContext db = new AssessoriaWebContext();

        // GET: PlanoAlimentares
        public ActionResult Index()
        {
            var planoAlimentar = db.PlanoAlimentars.Include(p => p.Atleta).Include(a => a.Atleta.Pessoa);
            planoAlimentar = planoAlimentar.Include(t => t.Nutricionista).Include(t => t.Nutricionista.Pessoa);
            return View(planoAlimentar.ToList());
        }

        // GET: PlanoAlimentares/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanoAlimentar planoAlimentar = db.PlanoAlimentars.Find(id);
            db.Entry(planoAlimentar).Reference(p => p.Atleta).Load();
            db.Entry(planoAlimentar.Atleta).Reference(p => p.Pessoa).Load();

            db.Entry(planoAlimentar).Reference(p => p.Nutricionista).Load();
            db.Entry(planoAlimentar.Nutricionista).Reference(p => p.Pessoa).Load();
            if (planoAlimentar == null)
            {
                return HttpNotFound();
            }
            return View(planoAlimentar);
        }

        // GET: PlanoAlimentares/Create
        public ActionResult Create()
        {
            ViewBag.atl_id = new SelectList(db.Atletas.Include(a => a.Pessoa), "atl_id", "Pessoa.pes_nome");
            ViewBag.nut_id = new SelectList(db.Nutricionistas.Include(a => a.Pessoa), "nut_id", "Pessoa.pes_nome");
            return View();
        }

        // POST: PlanoAlimentares/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "pln_id,pln_datainicial,pln_datafinal,pla_plano,atl_id,nut_id")] PlanoAlimentar planoAlimentar)
        {
            if (ModelState.IsValid)
            {
                db.PlanoAlimentars.Add(planoAlimentar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.atl_id = new SelectList(db.Atletas.Include(a => a.Pessoa), "atl_id", "Pessoa.pes_nome", planoAlimentar.atl_id);
            ViewBag.nut_id = new SelectList(db.Nutricionistas.Include(a => a.Pessoa), "nut_id", "Pessoa.pes_nome", planoAlimentar.nut_id);
            return View(planoAlimentar);
        }

        // GET: PlanoAlimentares/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanoAlimentar planoAlimentar = db.PlanoAlimentars.Find(id);
            if (planoAlimentar == null)
            {
                return HttpNotFound();
            }

            ViewBag.atl_id = new SelectList(db.Atletas.Include(a => a.Pessoa), "atl_id", "Pessoa.pes_nome", planoAlimentar.atl_id);
            ViewBag.nut_id = new SelectList(db.Nutricionistas.Include(a => a.Pessoa), "nut_id", "Pessoa.pes_nome", planoAlimentar.nut_id);
            return View(planoAlimentar);
        }

        // POST: PlanoAlimentares/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "pln_id,pln_datainicial,pln_datafinal,pla_plano,atl_id,nut_id")] PlanoAlimentar planoAlimentar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planoAlimentar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.atl_id = new SelectList(db.Atletas.Include(a => a.Pessoa), "atl_id", "Pessoa.pes_nome", planoAlimentar.atl_id);
            ViewBag.nut_id = new SelectList(db.Nutricionistas.Include(a => a.Pessoa), "nut_id", "Pessoa.pes_nome", planoAlimentar.nut_id);
            return View(planoAlimentar);
        }

        // GET: PlanoAlimentares/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanoAlimentar planoAlimentar = db.PlanoAlimentars.Find(id);
            if (planoAlimentar == null)
            {
                return HttpNotFound();
            }
            return View(planoAlimentar);
        }

        // POST: PlanoAlimentares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlanoAlimentar planoAlimentar = db.PlanoAlimentars.Find(id);
            db.PlanoAlimentars.Remove(planoAlimentar);
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
