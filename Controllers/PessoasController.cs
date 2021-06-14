using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssessoriaWeb.Models;
using static AssessoriaWeb.Helpers.CustomHelpers;

namespace AssessoriaWeb.Controllers
{
    [Authorize(Roles = "admin,assessor")]
    public class PessoasController : Controller
    {
        private AssessoriaWebContext db = new AssessoriaWebContext();

        // GET: Pessoas
        public ActionResult Index()
        {
            var pessoas = db.Pessoas;
            return View(pessoas.ToList());
        }

        // GET: Pessoas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Pessoa pessoa = db.Pessoas.Find(id);

            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        // GET: Pessoas/Create
        public ActionResult Create()
        {
            IList<CustomSelectItem> list_sexo = new List<CustomSelectItem>(new[] {
                new CustomSelectItem { Value = "F", SelectedValue = "Feminino", Text = "Feminino"},
                new CustomSelectItem { Value = "M", SelectedValue = "Masculino", Text = "Masculino"}
            });
            ViewBag.list_sexo = list_sexo;
            return View();
        }

        // POST: Pessoas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pes_id,pes_nome,pes_cpf,pes_datanascimento,pes_telefone,pes_email,pes_login,pes_senha,pes_sexo")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                db.Pessoas.Add(pessoa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pessoa);
        }

        // GET: Pessoas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Pessoa pessoa = db.Pessoas.Find(id);

            IList<CustomSelectItem> list_sexo = new List<CustomSelectItem>(new[] {
                new CustomSelectItem { Value = "F", SelectedValue = "Feminino", Text = "Feminino", Selected = pessoa.pes_sexo == "F"},
                new CustomSelectItem { Value = "M", SelectedValue = "Masculino", Text = "Masculino", Selected = pessoa.pes_sexo == "F"}
            });
            ViewBag.list_sexo = list_sexo;
            ViewBag.sexoSelecionado = pessoa.pes_sexo;
            if (pessoa == null)
            {
                return HttpNotFound();
            }

            return View(pessoa);
        }

        // POST: Pessoas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pes_id,pes_nome,pes_cpf,pes_datanascimento,pes_telefone,pes_email,pes_login,pes_senha,pes_sexo")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pessoa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pessoa);
        }

        // GET: Pessoas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Pessoas.Find(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }

            return View(pessoa);
        }

        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pessoa pessoa = db.Pessoas.Find(id);

            db.Entry(pessoa).Collection(p => p.Assessores).Load();
            if (pessoa.Assessores.Count > 0)
            {
                ViewBag.Message = "Não pode excluir, pois possui Assessores relacionados com esse Usuário";
                return View(pessoa);
            }

            db.Entry(pessoa).Collection(p => p.Atletas).Load();
            if (pessoa.Atletas.Count > 0)
            {
                ViewBag.Message = "Não pode excluir, pois possui Atletas relacionados com esse Usuário";
                return View(pessoa);
            }

            db.Entry(pessoa).Collection(p => p.Nutricionistas).Load();
            if (pessoa.Nutricionistas.Count > 0)
            {
                ViewBag.Message = "Não pode excluir, pois possui Nutricionistas relacionados com esse Usuário";
                return View(pessoa);
            }

            db.Entry(pessoa).Collection(p => p.Enderecos).Load();
            if (pessoa.Enderecos.Count > 0)
            {
                ViewBag.Message = "Não pode excluir, pois possui Endereços relacionados com esse Usuário";
                return View(pessoa);
            }

            db.Pessoas.Remove(pessoa);
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
