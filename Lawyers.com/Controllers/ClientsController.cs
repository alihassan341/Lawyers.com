using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Lawyers.com.Models;

namespace lawyers.Controllers
{
    public class ClientController : Controller
    {
        lawyersEntities db = new lawyersEntities();
        // GET: Client
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(client C)
        {
            if (ModelState.IsValid == true)
            {
                db.clients.Add(C);
                int x = db.SaveChanges();
                if (x > 0)
                {
                    TempData["InsertMsg"] = "<script>alert('Register is succesully')</script>";
                }
                else
                {
                    TempData["InsertMsg"] = "<script>alert('Something Went Wrong')</script>";
                }
                return RedirectToAction("login");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(client C)
        {
            var client = db.clients.Where(model => model.Email == C.Email && model.Password == C.Password).FirstOrDefault();
            if (client != null)
            {
                Session["Cid"] = C.Cid.ToString();
                Session["Email"] = C.Email.ToString();
                TempData["LoginSuccessmassage"] = "<script>alert('Login Success')</script>";
                return RedirectToAction("ClientProfile");
            }
            else
            {
                return View();
            }
           
        }

        public ActionResult ClientProfile()
        {
            if (Session["Cid"] == null)
            {
                return RedirectToAction("login");
            }
            else
            {
                return View();
            }
        }
        public ActionResult logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        
    }
}