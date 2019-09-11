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
    public class StudentFamiliesController : Controller
    {
        private Entities db = new Entities();

        // GET: StudentFamilies
        public ActionResult Index()
        {
            var studentFamilies = db.StudentFamilies.Include(s => s.Student);
            return View(studentFamilies.ToList());
        }

        // GET: StudentFamilies/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentFamily studentFamily = db.StudentFamilies.Find(id);
            if (studentFamily == null)
            {
                return HttpNotFound();
            }
            return View(studentFamily);
        }

        // GET: StudentFamilies/Create
        public ActionResult Create()
        {

            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName");
            return View();
        }

        // POST: StudentFamilies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ParentID,StudentID,FatherFirstName,FatherLastName,MotherFirstName,MotherLastName,ParentEmail,ParentPhoneNo")] StudentFamily studentFamily)
        {
            if (ModelState.IsValid)
            {
                db.StudentFamilies.Add(studentFamily);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", studentFamily.StudentID);
            return View(studentFamily);
        }

        // GET: StudentFamilies/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentFamily studentFamily = db.StudentFamilies.Find(id);
            if (studentFamily == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", studentFamily.StudentID);
            return View(studentFamily);
        }

        // POST: StudentFamilies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ParentID,StudentID,FatherFirstName,FatherLastName,MotherFirstName,MotherLastName,ParentEmail,ParentPhoneNo")] StudentFamily studentFamily)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentFamily).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", studentFamily.StudentID);
            return View(studentFamily);
        }

        // GET: StudentFamilies/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentFamily studentFamily = db.StudentFamilies.Find(id);
            if (studentFamily == null)
            {
                return HttpNotFound();
            }
            return View(studentFamily);
        }

        // POST: StudentFamilies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            StudentFamily studentFamily = db.StudentFamilies.Find(id);
            db.StudentFamilies.Remove(studentFamily);
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
