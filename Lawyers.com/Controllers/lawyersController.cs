using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Lawyers.com.Models;

namespace lawyers.Controllers
{
    public class LawyerController : Controller
    {
        lawyersEntities db = new lawyersEntities();
        // GET: Lawyer
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(lawyer L)
        {
            if (ModelState.IsValid == true)
            {
                db.lawyers.Add(L);
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

        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(lawyer L)
        {
            var lawyer = db.lawyers.Where(model => model.Email == L.Email && model.Password == L.Password).FirstOrDefault();
            if (lawyer != null)
            {
                Session["Lid"] = L.Lid.ToString();
                Session["Email"] = L.Email.ToString();
                TempData["LoginSuccessmassage"] = "<script>alert('Login Success')</script>";
                return RedirectToAction("LawyerProfile");
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
        public ActionResult LawyerProfile()
        {
            if (Session["Lid"] == null)
            {
                return RedirectToAction("login");
            }
            else
            {
                return View();
            }
        }
         
        }
       
    }
