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
    public class CashWithdrawlsController : Controller
    {
        private BullshitTrackerEntities db = new BullshitTrackerEntities();

        // GET: CashWithdrawls
        public ActionResult Index()
        {
            var cashWithdrawls = db.CashWithdrawls.Include(c => c.Account).Include(c => c.Account1);
            return View(cashWithdrawls.ToList());
        }

        // GET: CashWithdrawls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashWithdrawl cashWithdrawl = db.CashWithdrawls.Find(id);
            if (cashWithdrawl == null)
            {
                return HttpNotFound();
            }
            return View(cashWithdrawl);
        }

        // GET: CashWithdrawls/Create
        public ActionResult Create()
        {
            ViewBag.FromAccount = new SelectList(db.Accounts, "Id", "Name");
            ViewBag.ToAccount = new SelectList(db.Accounts, "Id", "Name");
            return View();
        }

        // POST: CashWithdrawls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DateWithdrawn,Amount,ToAccount,FromAccount")] CashWithdrawl cashWithdrawl)
        {
            if (ModelState.IsValid)
            {
                db.CashWithdrawls.Add(cashWithdrawl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FromAccount = new SelectList(db.Accounts, "Id", "Name", cashWithdrawl.FromAccount);
            ViewBag.ToAccount = new SelectList(db.Accounts, "Id", "Name", cashWithdrawl.ToAccount);
            return View(cashWithdrawl);
        }

        // GET: CashWithdrawls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashWithdrawl cashWithdrawl = db.CashWithdrawls.Find(id);
            if (cashWithdrawl == null)
            {
                return HttpNotFound();
            }
            ViewBag.FromAccount = new SelectList(db.Accounts, "Id", "Name", cashWithdrawl.FromAccount);
            ViewBag.ToAccount = new SelectList(db.Accounts, "Id", "Name", cashWithdrawl.ToAccount);
            return View(cashWithdrawl);
        }

        // POST: CashWithdrawls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DateWithdrawn,Amount,ToAccount,FromAccount")] CashWithdrawl cashWithdrawl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cashWithdrawl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FromAccount = new SelectList(db.Accounts, "Id", "Name", cashWithdrawl.FromAccount);
            ViewBag.ToAccount = new SelectList(db.Accounts, "Id", "Name", cashWithdrawl.ToAccount);
            return View(cashWithdrawl);
        }

        // GET: CashWithdrawls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashWithdrawl cashWithdrawl = db.CashWithdrawls.Find(id);
            if (cashWithdrawl == null)
            {
                return HttpNotFound();
            }
            return View(cashWithdrawl);
        }

        // POST: CashWithdrawls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CashWithdrawl cashWithdrawl = db.CashWithdrawls.Find(id);
            db.CashWithdrawls.Remove(cashWithdrawl);
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
