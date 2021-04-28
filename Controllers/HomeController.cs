using AssessoriaWeb.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AssessoriaWeb.Controllers
{
    [AllowAnonymous]
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
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Login(Pessoa pessoa)
        {
            string redirecionar = "Login";
            if (ModelState.IsValid)
            {
                Pessoa retorno = db.Pessoas.Where(x => x.pes_login.Equals(pessoa.pes_login) && x.pes_senha.Equals(pessoa.pes_senha)).Include(p => p.TipoPessoa).FirstOrDefault();
                if (retorno == null)
                {
                    //login inválido
                    ViewBag.Message = "Usuário ou senha inválida";
                    return View();
                }
                dynamic user = new ExpandoObject();
                user.login = retorno.pes_login;
                user.autenticado = true;

                switch (retorno.tpp_id)
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
                System.Web.Security.FormsAuthentication.SetAuthCookie(user.login, false);

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