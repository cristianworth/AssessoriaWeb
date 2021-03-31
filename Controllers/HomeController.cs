using AssessoriaWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssessoriaWeb.Controllers
{
    public class HomeController : Controller
    {
        private AssessoriaWebContext db = new AssessoriaWebContext();
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }

            [HttpPost]
        public ActionResult Login(Pessoa p)
        {
            string redirecionar = "Login";
            if (ModelState.IsValid)
            {
                var retorno = db.Pessoas.Where(x => x.pes_login.Equals(p.pes_login) && x.pes_senha.Equals(p.pes_senha)).FirstOrDefault();
                if (retorno == null)
                {
                    //login inválido
                    ViewBag.Message = "Usuário ou senha inválida";
                    return View();
                }
                else
                {
                    switch (retorno.pes_tipo)
                    {
                        case 1:
                            redirecionar = "Index"; //redirecionar = "AtletaDashBoard";
                            break;
                        case 2:
                            redirecionar = "Index"; //redirecionar = "AssessorDashBoard";
                            break;
                        default:
                            redirecionar = "Index"; //redirecionar = "NutricionistaDashBoard";
                            break;
                    }
                    Session["PessoaId"] = retorno.pes_id.ToString();
                    Session["PessoaNome"] = retorno.pes_nome;
                }
            }
            return RedirectToAction(redirecionar);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}