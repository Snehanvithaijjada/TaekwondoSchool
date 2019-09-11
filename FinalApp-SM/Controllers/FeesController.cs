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
    public class FeesController : Controller
    {
        private Entities db = new Entities();

        // GET: Fees
        public ActionResult Index(String StudentID)
        {
            var fees = db.Fees.Include(f => f.Class).Include(f => f.Product).Include(f => f.Student);
            if (!string.IsNullOrEmpty(StudentID))
            {
                fees = fees.Where(x => x.StudentID == StudentID);
            }
            return View(fees.ToList());
        }

        // GET: Fees/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fee fee = db.Fees.Find(id);
            if (fee == null)
            {
                return HttpNotFound();
            }
            return View(fee);
        }

        // GET: Fees/Create
        public ActionResult Create()
        {
            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "ClassType");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName");
            return View();
        }

        // POST: Fees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FeeID,StudentID,ClassID,ProductID,Amount,PaymentDate")] Fee fee)
        {
            if (ModelState.IsValid)
            {
                db.Fees.Add(fee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "ClassType", fee.ClassID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", fee.ProductID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", fee.StudentID);
            return View(fee);
        }

        // GET: Fees/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fee fee = db.Fees.Find(id);
            if (fee == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "ClassType", fee.ClassID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", fee.ProductID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", fee.StudentID);
            return View(fee);
        }

        // POST: Fees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FeeID,StudentID,ClassID,ProductID,Amount,PaymentDate")] Fee fee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "ClassType", fee.ClassID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", fee.ProductID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", fee.StudentID);
            return View(fee);
        }

        // GET: Fees/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fee fee = db.Fees.Find(id);
            if (fee == null)
            {
                return HttpNotFound();
            }
            return View(fee);
        }

        // POST: Fees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Fee fee = db.Fees.Find(id);
            db.Fees.Remove(fee);
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
