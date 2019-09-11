using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalApp_SM.Models;

namespace FinalApp_SM.Controllers
{
    public class StudentAttendancesController : Controller
    {
        private Entities db = new Entities();

        // GET: StudentAttendances
        public ActionResult Index(String StudentID)
        {
            var studentAttendances = db.StudentAttendances.Include(s => s.Class).Include(s => s.Student);
            if (!string.IsNullOrEmpty(StudentID))
            {
                studentAttendances = studentAttendances.Where(x => x.StudentID == StudentID);
            }
            return View(studentAttendances.ToList());
        }

        // GET: StudentAttendances/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAttendance studentAttendance = db.StudentAttendances.Find(id);
            if (studentAttendance == null)
            {
                return HttpNotFound();
            }
            return View(studentAttendance);
        }

        // GET: StudentAttendances/Create
        public ActionResult Create()
        {
            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "ClassType");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName");
            return View();
        }

        // POST: StudentAttendances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AttendanceID,StudentID,ClassID,AttendanceTakenDate,IsPresent")] StudentAttendance studentAttendance)
        {
            if (ModelState.IsValid)
            {
                db.StudentAttendances.Add(studentAttendance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "ClassType", studentAttendance.ClassID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", studentAttendance.StudentID);
            return View(studentAttendance);
        }

        // GET: StudentAttendances/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAttendance studentAttendance = db.StudentAttendances.Find(id);
            if (studentAttendance == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "ClassType", studentAttendance.ClassID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", studentAttendance.StudentID);
            return View(studentAttendance);
        }

        // POST: StudentAttendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AttendanceID,StudentID,ClassID,AttendanceTakenDate,IsPresent")] StudentAttendance studentAttendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentAttendance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "ClassType", studentAttendance.ClassID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", studentAttendance.StudentID);
            return View(studentAttendance);
        }

        // GET: StudentAttendances/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAttendance studentAttendance = db.StudentAttendances.Find(id);
            if (studentAttendance == null)
            {
                return HttpNotFound();
            }
            return View(studentAttendance);
        }

        // POST: StudentAttendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            StudentAttendance studentAttendance = db.StudentAttendances.Find(id);
            db.StudentAttendances.Remove(studentAttendance);
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
