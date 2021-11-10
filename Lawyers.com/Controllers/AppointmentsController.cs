using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Lawyers.com.Models;


namespace Lawyers.com.Controllers
{
    public class AppointmentsController : Controller
    {
        lawyersEntities db = new lawyersEntities();

        public string Cancelled { get; private set; }

        // GET: Appointments
        public ActionResult Appointment()
        {
            if (Session["Cid"] == null)
            {
                return RedirectToAction("login", "client");
            }
            ViewBag.Cid = new SelectList(db.clients, "Cid", "First_Name");
            ViewBag.Lid = new SelectList(db.lawyers, "Lid", "First_Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Appointment([Bind(Include = "id,Name,Email,Phone,Date,Issue,Lid,Cid")] Appointment a)
        {
            if (ModelState.IsValid == true)
            {
                a.Status = "Pending";
                db.Appointments.Add(a);
                int x = db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("ClientProfile","client");
            }
                ViewBag.Cid = new SelectList(db.clients, "Cid", "First_Name", a.Cid);
                ViewBag.Lid = new SelectList(db.lawyers, "Lid", "First_Name", a.Lid);
        
              return View();
                      
        }

        public ActionResult AppList()
        {
           if(Session["Lid"] == null)
           {
              return RedirectToAction("login","lawyer");
           }

            var show = db.Appointments.ToList();

            return View(show);
        }

        public ActionResult Delete(int id)
        {
            var D = db.Appointments.Where(x => x.id == id).First();
            db.Appointments.Remove(D);
           
            db.SaveChanges();
            var list = db.Appointments.ToList();
            return View("AppList", list);
        }

        public ActionResult Edit(int id)
        {     
            var E = db.Appointments.Where(x => x.id == id).First();
            E.Status = "Approved";
            
            db.SaveChanges();
            var list = db.Appointments.ToList();
            return View("AppList",list);
            }
            
      }

}
