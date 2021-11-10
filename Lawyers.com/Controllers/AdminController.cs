using Lawyers.com.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lawyers.com.Controllers
{
    public class AdminController : Controller
    {
        lawyersEntities db = new lawyersEntities();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("login");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Admin A)
        {   
            if(ModelState.IsValid == true)
            {
                db.Admins.Add(A);
                db.SaveChanges();
                return RedirectToAction("login");
            }
            return View();
        }

        public ActionResult login()
        {
            return View();
         }

        [HttpPost]
        public ActionResult login(Admin A)
        {
            var admins = db.Admins.Where(model => model.username == A.username && model.Password == A.Password).FirstOrDefault();
            if(admins!=null)
            {
                Session["id"] = A.id.ToString();
                Session["UerName"] = A.username.ToString();
                return RedirectToAction("index");
            }
           else
            return View();
        }

        public ActionResult logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult lawyerslist()
        {
            var list = db.lawyers.ToList();            
            return View(list);
        }
        public ActionResult clientslist()
        {
            var list = db.clients.ToList();
            return View(list);
        }

        public ActionResult AppList()
        {
            //if(Session["id"] == null)
            //{
            //    return RedirectToAction("login","lawyer");
            //}

            var show = db.Appointments.ToList();

            return View(show);
        }
    }
}