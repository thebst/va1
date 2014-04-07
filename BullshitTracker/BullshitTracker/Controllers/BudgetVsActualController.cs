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
    [Authorize(Users = @"MarkDeganiLocalhost, MarkDegani, RebeccaDegani")]
    public class BudgetVsActualController : Controller
    {
        private BullshitTrackerEntities db = new BullshitTrackerEntities();

        // GET: /BudgetVsActual/
        public ActionResult Index(int periodId)
        {
            ViewBag.PeriodName = db.Periods.Find(periodId).Name.ToString();

            return View(db.PeriodBudgetVsActuals.ToList().Where(n => n.PeriodId == periodId));


        }

        // GET: /BudgetVsActual/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PeriodBudgetVsActual periodbudgetvsactual = db.PeriodBudgetVsActuals.Find(id);
            if (periodbudgetvsactual == null)
            {
                return HttpNotFound();
            }
            return View(periodbudgetvsactual);
        }

        // GET: /BudgetVsActual/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /BudgetVsActual/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,PeriodId,PeriodName,StartDate,EndDate,BudgetId,BudgetName,ActualAmount,BudgetedAmount,PercentOfBudget,Month,Year")] PeriodBudgetVsActual periodbudgetvsactual)
        {
            if (ModelState.IsValid)
            {
                db.PeriodBudgetVsActuals.Add(periodbudgetvsactual);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(periodbudgetvsactual);
        }

        // GET: /BudgetVsActual/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PeriodBudgetVsActual periodbudgetvsactual = db.PeriodBudgetVsActuals.Find(id);
            if (periodbudgetvsactual == null)
            {
                return HttpNotFound();
            }
            return View(periodbudgetvsactual);
        }

        // POST: /BudgetVsActual/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,PeriodId,PeriodName,StartDate,EndDate,BudgetId,BudgetName,ActualAmount,BudgetedAmount,PercentOfBudget,Month,Year")] PeriodBudgetVsActual periodbudgetvsactual)
        {
            if (ModelState.IsValid)
            {
                db.Entry(periodbudgetvsactual).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(periodbudgetvsactual);
        }

        // GET: /BudgetVsActual/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PeriodBudgetVsActual periodbudgetvsactual = db.PeriodBudgetVsActuals.Find(id);
            if (periodbudgetvsactual == null)
            {
                return HttpNotFound();
            }
            return View(periodbudgetvsactual);
        }

        // POST: /BudgetVsActual/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PeriodBudgetVsActual periodbudgetvsactual = db.PeriodBudgetVsActuals.Find(id);
            db.PeriodBudgetVsActuals.Remove(periodbudgetvsactual);
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
