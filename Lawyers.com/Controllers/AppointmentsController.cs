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
                a.Status = "pending";
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
            ViewBag.Cid = new SelectList(db.clients, "Cid", "First_Name", a.Cid);
            ViewBag.Lid = new SelectList(db.lawyers, "Lid", "First_Name", a.Lid);
            return View();
        }

        public ActionResult AppList()
        {
            var show = db.Appointments.ToList();

            return View(show);
        }  
        //[HttpPost]
        //public ActionResult AppList([Bind(Include = "id,Name,Email,Phone,Date,Issue,Status,Lid,Cid")] int id, string abs)
        //{
        //    Appointment appointment = db.Appointments.Find(id);

        //    db.Entry(appointment).State = EntityState.Modified;
        //    db.SaveChanges();
        //    var show = db.Appointments.ToList();

        //    return View(show);
        //}

       
        public ActionResult Delete(int id)
        {
            var D = db.Appointments.Where(x => x.id == id).First();
            db.Appointments.Remove(D);
           
            db.SaveChanges();
            var list = db.Appointments.ToList();
            return View("AppList", list);
        }

        public ActionResult Edit([Bind(Include = "id,Name,Email,Phone,Date,Issue,Status,Lid,Cid")] int id)
        {
            if (id > 0)
            {
                Appointment appointment = db.Appointments.Find(id);

                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View();
        }

    }
}