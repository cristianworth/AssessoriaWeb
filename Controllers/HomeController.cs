using AssessoriaWeb.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
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
            string action = "Index";
            string controller = "Home";
            if (ModelState.IsValid)
            {
                pessoa = db.Pessoas.Where(x => x.pes_login.Equals(pessoa.pes_login) && x.pes_senha.Equals(pessoa.pes_senha)).FirstOrDefault();
                if (pessoa == null)
                {
                    //login inválido
                    ViewBag.Message = "Usuário ou senha inválida";
                    return View();
                }
                db.Entry(pessoa).Collection(p => p.Assessores).Load();
                db.Entry(pessoa).Collection(p => p.Atletas).Load();
                db.Entry(pessoa).Collection(p => p.Nutricionistas).Load();
                List<string> roles = new List<string>();
                if (pessoa.Assessores.Count() > 0)
                {
                    roles.Add("assessor");
                }
                if (pessoa.Atletas.Count() > 0)
                {
                    roles.Add("atleta");
                     action = "Index";
                     controller = "DashBoard";
                }  
                if (pessoa.Nutricionistas.Count() > 0)
                {
                    roles.Add("nutricionista");
                }
                if (pessoa.pes_login == "admin")
                {
                    roles.Add("admin");
                }

                Session["pes_id"] = pessoa.pes_id.ToString();
                Session["pes_nome"] = pessoa.pes_nome;
                Session["pes_login"] = pessoa.pes_login;
                Session["roles"] = roles.ToArray();

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                        1,
                        pessoa.pes_login,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(720),
                        true,
                        serializer.Serialize(roles.ToArray()));

                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                Response.Cookies.Add(faCookie);
                //System.Web.Security.FormsAuthentication.SetAuthCookie(pessoa.pes_login, true);

            }
            return RedirectToAction(action, controller);
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
            ViewBag.Message = "Fale Com a Gente";

            return View();
        }
    }
}