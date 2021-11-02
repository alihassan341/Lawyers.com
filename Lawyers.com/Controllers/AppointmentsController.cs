using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lawyers.com.Models;



namespace Lawyers.com.Controllers
{
    public class AppointmentsController : Controller
    {
        lawyersEntities db = new lawyersEntities();
        // GET: Appointments
        public ActionResult Appointment()
        {
           return View();
        }
        [HttpPost]
        public ActionResult Appointment(Appointment a)
        {
            if (Session["Cid"] == null)
            {
                return RedirectToAction("login", "client");
            }

            if (ModelState.IsValid == true)
            {
                db.Appointments.Add(a);
                int x = db.SaveChanges();
                if (x > 0)
                {
                    TempData["Appoinmentmsg"] = "<script>alert('your Appointment is Book')</script>";
                }
                else
                {
                    TempData["InsertMsg"] = "<script>alert('Something Went Wrong')</script>";
                }
               
            }               
              return View();
        }

    }
}