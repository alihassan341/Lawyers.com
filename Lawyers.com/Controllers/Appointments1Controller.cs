using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lawyers.com.Models;

namespace Lawyers.com.Controllers
{
    public class Appointments1Controller : Controller
    {
        private lawyersEntities db = new lawyersEntities();

        // GET: Appointments1
        public ActionResult Index()
        {
            var appointments = db.Appointments.Include(a => a.client).Include(a => a.lawyer);
            return View(appointments.ToList());
        }

        // GET: Appointments1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointments1/Create
        public ActionResult Create()
        {
            ViewBag.Cid = new SelectList(db.clients, "Cid", "First_Name");
            ViewBag.Lid = new SelectList(db.lawyers, "Lid", "First_Name");
            return View();
        }

        // POST: Appointments1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name,Email,Phone,Date,Issue,Status,Lid,Cid")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cid = new SelectList(db.clients, "Cid", "First_Name", appointment.Cid);
            ViewBag.Lid = new SelectList(db.lawyers, "Lid", "First_Name", appointment.Lid);
            return View(appointment);
        }

        // GET: Appointments1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cid = new SelectList(db.clients, "Cid", "First_Name", appointment.Cid);
            ViewBag.Lid = new SelectList(db.lawyers, "Lid", "First_Name", appointment.Lid);
            return View(appointment);
        }

        // POST: Appointments1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,Email,Phone,Date,Issue,Status,Lid,Cid")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cid = new SelectList(db.clients, "Cid", "First_Name", appointment.Cid);
            ViewBag.Lid = new SelectList(db.lawyers, "Lid", "First_Name", appointment.Lid);
            return View(appointment);
        }

        // GET: Appointments1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
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
