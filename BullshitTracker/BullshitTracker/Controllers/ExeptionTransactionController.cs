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
    public class ExeptionTransactionController : Controller
    {
        private BullshitTrackerEntities db = new BullshitTrackerEntities();

        // GET: /ExeptionTransaction/
        public ActionResult Index()
        {
            var exceptiontransactions = db.ExceptionTransactions.Include(e => e.Category1).Include(e => e.Transaction).Include(e => e.TaxRate1);
            return View(exceptiontransactions.ToList());
        }

        // GET: /ExeptionTransaction/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExceptionTransaction exceptiontransaction = db.ExceptionTransactions.Find(id);
            if (exceptiontransaction == null)
            {
                return HttpNotFound();
            }
            return View(exceptiontransaction);
        }

        // GET: /ExeptionTransaction/Create
        public ActionResult Create()
        {
            ViewBag.Category = new SelectList(db.Categories, "Id", "Name");
            ViewBag.TransactionId = new SelectList(db.Transactions, "Id", "Description");
            ViewBag.TaxRate = new SelectList(db.TaxRates, "Id", "Description");
            return View();
        }

        // POST: /ExeptionTransaction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,TransactionId,Description,AmountPreTax,HST,Category,Shame,TaxRate")] ExceptionTransaction exceptiontransaction)
        {
            if (ModelState.IsValid)
            {
                db.ExceptionTransactions.Add(exceptiontransaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category = new SelectList(db.Categories, "Id", "Name", exceptiontransaction.Category);
            ViewBag.TransactionId = new SelectList(db.Transactions, "Id", "Description", exceptiontransaction.TransactionId);
            ViewBag.TaxRate = new SelectList(db.TaxRates, "Id", "Description", exceptiontransaction.TaxRate);
            return View(exceptiontransaction);
        }

        // GET: /ExeptionTransaction/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExceptionTransaction exceptiontransaction = db.ExceptionTransactions.Find(id);
            if (exceptiontransaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category = new SelectList(db.Categories, "Id", "Name", exceptiontransaction.Category);
            ViewBag.TransactionId = new SelectList(db.Transactions, "Id", "Description", exceptiontransaction.TransactionId);
            ViewBag.TaxRate = new SelectList(db.TaxRates, "Id", "Description", exceptiontransaction.TaxRate);
            return View(exceptiontransaction);
        }

        // POST: /ExeptionTransaction/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,TransactionId,Description,AmountPreTax,HST,Category,Shame,TaxRate")] ExceptionTransaction exceptiontransaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exceptiontransaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category = new SelectList(db.Categories, "Id", "Name", exceptiontransaction.Category);
            ViewBag.TransactionId = new SelectList(db.Transactions, "Id", "Description", exceptiontransaction.TransactionId);
            ViewBag.TaxRate = new SelectList(db.TaxRates, "Id", "Description", exceptiontransaction.TaxRate);
            return View(exceptiontransaction);
        }

        // GET: /ExeptionTransaction/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExceptionTransaction exceptiontransaction = db.ExceptionTransactions.Find(id);
            if (exceptiontransaction == null)
            {
                return HttpNotFound();
            }
            return View(exceptiontransaction);
        }

        // POST: /ExeptionTransaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExceptionTransaction exceptiontransaction = db.ExceptionTransactions.Find(id);
            db.ExceptionTransactions.Remove(exceptiontransaction);
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
