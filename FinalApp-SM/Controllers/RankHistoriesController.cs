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
    public class RankHistoriesController : Controller
    {
        private Entities db = new Entities();

        // GET: RankHistories
        public ActionResult Index(String StudentID)
        {
            var rankHistories = db.RankHistories.Include(r => r.Rank).Include(r => r.Student);
            if (!string.IsNullOrEmpty(StudentID))
            {
                rankHistories = rankHistories.Where(x => x.StudentID == StudentID);
            }
            return View(rankHistories.ToList());
        }

        // GET: RankHistories/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RankHistory rankHistory = db.RankHistories.Find(id);
            if (rankHistory == null)
            {
                return HttpNotFound();
            }
            return View(rankHistory);
        }

        // GET: RankHistories/Create
        public ActionResult Create()
        {
            ViewBag.RankID = new SelectList(db.Ranks, "RankID", "BeltColor");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName");
            return View();
        }

        // POST: RankHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HistoryID,StudentID,RankID,RankDate")] RankHistory rankHistory)
        {
            if (ModelState.IsValid)
            {
                db.RankHistories.Add(rankHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RankID = new SelectList(db.Ranks, "RankID", "BeltColor", rankHistory.RankID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", rankHistory.StudentID);
            return View(rankHistory);
        }

        // GET: RankHistories/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RankHistory rankHistory = db.RankHistories.Find(id);
            if (rankHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.RankID = new SelectList(db.Ranks, "RankID", "BeltColor", rankHistory.RankID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", rankHistory.StudentID);
            return View(rankHistory);
        }

        // POST: RankHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HistoryID,StudentID,RankID,RankDate")] RankHistory rankHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rankHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RankID = new SelectList(db.Ranks, "RankID", "BeltColor", rankHistory.RankID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", rankHistory.StudentID);
            return View(rankHistory);
        }

        // GET: RankHistories/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RankHistory rankHistory = db.RankHistories.Find(id);
            if (rankHistory == null)
            {
                return HttpNotFound();
            }
            return View(rankHistory);
        }

        // POST: RankHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            RankHistory rankHistory = db.RankHistories.Find(id);
            db.RankHistories.Remove(rankHistory);
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
