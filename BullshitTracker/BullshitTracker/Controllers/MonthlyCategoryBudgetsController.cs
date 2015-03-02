using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BullshitTracker.Models;

namespace BullshitTracker.Controllers
{
    public class MonthlyCategoryBudgetsController : Controller
    {
        private BullshitTrackerEntities db = new BullshitTrackerEntities();

        // GET: MonthlyCategoryBudgets
        public ActionResult Index()
        {
            var monthlyCategoryBudgets = db.MonthlyCategoryBudgets.Include(m => m.Category1).Include(m => m.Period1);
            return View(monthlyCategoryBudgets.ToList());
        }

        // GET: MonthlyCategoryBudgets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyCategoryBudget monthlyCategoryBudget = db.MonthlyCategoryBudgets.Find(id);
            if (monthlyCategoryBudget == null)
            {
                return HttpNotFound();
            }
            return View(monthlyCategoryBudget);
        }

        // GET: MonthlyCategoryBudgets/Create
        public ActionResult Create()
        {
            ViewBag.Category = new SelectList(db.Categories, "Id", "Name");
            ViewBag.Period = new SelectList(db.Periods, "Id", "Name");
            return View();
        }

        // POST: MonthlyCategoryBudgets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Period,Category,Amount")] MonthlyCategoryBudget monthlyCategoryBudget)
        {
            if (ModelState.IsValid)
            {
                db.MonthlyCategoryBudgets.Add(monthlyCategoryBudget);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category = new SelectList(db.Categories, "Id", "Name", monthlyCategoryBudget.Category);
            ViewBag.Period = new SelectList(db.Periods, "Id", "Name", monthlyCategoryBudget.Period);
            return View(monthlyCategoryBudget);
        }

        // GET: MonthlyCategoryBudgets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyCategoryBudget monthlyCategoryBudget = db.MonthlyCategoryBudgets.Find(id);
            if (monthlyCategoryBudget == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category = new SelectList(db.Categories, "Id", "Name", monthlyCategoryBudget.Category);
            ViewBag.Period = new SelectList(db.Periods, "Id", "Name", monthlyCategoryBudget.Period);
            return View(monthlyCategoryBudget);
        }

        // POST: MonthlyCategoryBudgets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Period,Category,Amount")] MonthlyCategoryBudget monthlyCategoryBudget)
        {
            if (ModelState.IsValid)
            {
                db.Entry(monthlyCategoryBudget).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category = new SelectList(db.Categories, "Id", "Name", monthlyCategoryBudget.Category);
            ViewBag.Period = new SelectList(db.Periods, "Id", "Name", monthlyCategoryBudget.Period);
            return View(monthlyCategoryBudget);
        }

        // GET: MonthlyCategoryBudgets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyCategoryBudget monthlyCategoryBudget = db.MonthlyCategoryBudgets.Find(id);
            if (monthlyCategoryBudget == null)
            {
                return HttpNotFound();
            }
            return View(monthlyCategoryBudget);
        }

        // POST: MonthlyCategoryBudgets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MonthlyCategoryBudget monthlyCategoryBudget = db.MonthlyCategoryBudgets.Find(id);
            db.MonthlyCategoryBudgets.Remove(monthlyCategoryBudget);
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
