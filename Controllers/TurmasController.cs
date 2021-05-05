﻿using System;
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
    public class TurmasController : Controller
    {
        private AssessoriaWebContext db = new AssessoriaWebContext();

        // GET: Turmas
        public ActionResult Index()
        {
            var turmas = db.Turmas.Include(t => t.Assessor);
            return View(turmas.ToList());
        }

        // GET: Turmas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turma turma = db.Turmas.Find(id);
            if (turma == null)
            {
                return HttpNotFound();
            }
            return View(turma);
        }

        // GET: Turmas/Create
        public ActionResult Create()
        {
            ViewBag.ass_id = new SelectList(db.Assessors, "ass_id", "ass_tipo");
            return View();
        }

        // POST: Turmas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "trm_id,trm_descricao,trm_observacao,trm_HoraInicial,trm_HoraFinal,ass_id")] Turma turma)
        {
            if (ModelState.IsValid)
            {
                db.Turmas.Add(turma);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ass_id = new SelectList(db.Assessors, "ass_id", "ass_tipo", turma.ass_id);
            return View(turma);
        }

        // GET: Turmas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turma turma = db.Turmas.Find(id);
            if (turma == null)
            {
                return HttpNotFound();
            }
            ViewBag.ass_id = new SelectList(db.Assessors, "ass_id", "ass_tipo", turma.ass_id);
            return View(turma);
        }

        // POST: Turmas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "trm_id,trm_descricao,trm_observacao,trm_HoraInicial,trm_HoraFinal,ass_id")] Turma turma)
        {
            if (ModelState.IsValid)
            {
                db.Entry(turma).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ass_id = new SelectList(db.Assessors, "ass_id", "ass_tipo", turma.ass_id);
            return View(turma);
        }

        // GET: Turmas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turma turma = db.Turmas.Find(id);
            if (turma == null)
            {
                return HttpNotFound();
            }
            return View(turma);
        }

        // POST: Turmas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Turma turma = db.Turmas.Find(id);
            db.Turmas.Remove(turma);
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