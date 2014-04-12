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
    public class TransactionsController : Controller
    {
        private BullshitTrackerEntities db = new BullshitTrackerEntities();

        // GET: /Transactions/
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.Account1).Include(t => t.Vendor1).OrderByDescending(n => n.TransactionDate);
            return View(transactions.ToList().OrderByDescending(n => n.TransactionDate));
        }

        // GET: /Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: /Transactions/Create
        public ActionResult Create()
        {
            ViewBag.Account = new SelectList(db.Accounts, "Id", "Name");
            ViewBag.Vendor = new SelectList(db.Vendors, "Id", "Name");
            return View();
        }

        // POST: /Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Description,TransactionDate,Vendor,Total,Account")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Account = new SelectList(db.Accounts, "Id", "Name", transaction.Account);
            ViewBag.Vendor = new SelectList(db.Vendors, "Id", "Name", transaction.Vendor);
            return View(transaction);
        }

        // GET: /Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.Account = new SelectList(db.Accounts, "Id", "Name", transaction.Account);
            ViewBag.Vendor = new SelectList(db.Vendors, "Id", "Name", transaction.Vendor);
            return View(transaction);
        }

        // POST: /Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Description,TransactionDate,Vendor,Total,Account")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Account = new SelectList(db.Accounts, "Id", "Name", transaction.Account);
            ViewBag.Vendor = new SelectList(db.Vendors, "Id", "Name", transaction.Vendor);
            return View(transaction);
        }

        // GET: /Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: /Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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
