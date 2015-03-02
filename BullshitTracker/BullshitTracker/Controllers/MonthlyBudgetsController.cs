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

      [Authorize(Roles = @"Planner")]
    public class MonthlyBudgetsController : Controller
    {
        private BullshitTrackerEntities db = new BullshitTrackerEntities();

        // GET: MonthlyBudgets
        public ActionResult Index()
        {
            var monthlyBudgets = db.MonthlyBudgets.Include(m => m.Budget1).Include(m => m.Period1);
            return View(monthlyBudgets.ToList());
        }

        // GET: MonthlyBudgets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyBudget monthlyBudget = db.MonthlyBudgets.Find(id);
            if (monthlyBudget == null)
            {
                return HttpNotFound();
            }
            return View(monthlyBudget);
        }

        // GET: MonthlyBudgets/Create
        public ActionResult Create()
        {
            ViewBag.Budget = new SelectList(db.Budgets, "Id", "BudgetName");
            ViewBag.Period = new SelectList(db.Periods, "Id", "Name");
            return View();
        }

        // POST: MonthlyBudgets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Period,Budget,Amount")] MonthlyBudget monthlyBudget)
        {
            if (ModelState.IsValid)
            {
                db.MonthlyBudgets.Add(monthlyBudget);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Budget = new SelectList(db.Budgets, "Id", "BudgetName", monthlyBudget.Budget);
            ViewBag.Period = new SelectList(db.Periods, "Id", "Name", monthlyBudget.Period);
            return View(monthlyBudget);
        }

        // GET: MonthlyBudgets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyBudget monthlyBudget = db.MonthlyBudgets.Find(id);
            if (monthlyBudget == null)
            {
                return HttpNotFound();
            }
            ViewBag.Budget = new SelectList(db.Budgets, "Id", "BudgetName", monthlyBudget.Budget);
            ViewBag.Period = new SelectList(db.Periods, "Id", "Name", monthlyBudget.Period);
            return View(monthlyBudget);
        }

        // POST: MonthlyBudgets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Period,Budget,Amount")] MonthlyBudget monthlyBudget)
        {
            if (ModelState.IsValid)
            {
                db.Entry(monthlyBudget).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Budget = new SelectList(db.Budgets, "Id", "BudgetName", monthlyBudget.Budget);
            ViewBag.Period = new SelectList(db.Periods, "Id", "Name", monthlyBudget.Period);
            return View(monthlyBudget);
        }

        // GET: MonthlyBudgets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyBudget monthlyBudget = db.MonthlyBudgets.Find(id);
            if (monthlyBudget == null)
            {
                return HttpNotFound();
            }
            return View(monthlyBudget);
        }

        // POST: MonthlyBudgets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MonthlyBudget monthlyBudget = db.MonthlyBudgets.Find(id);
            db.MonthlyBudgets.Remove(monthlyBudget);
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
