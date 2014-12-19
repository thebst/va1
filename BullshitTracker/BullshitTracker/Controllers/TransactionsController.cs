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
    [Authorize(Users = @"MarkDeganiLocalhost, MarkDegani, RebeccaDegani, Mdegani")]
    public class TransactionsController : Controller
    {
        private BullshitTrackerEntities db = new BullshitTrackerEntities();

        // GET: /Transactions/
        public ActionResult Index()
        {
            ViewBag.Account = new SelectList(db.Accounts.OrderBy(n => n.Name), "Id", "Name");
            ViewBag.Vendor = new SelectList(db.Vendors.OrderBy(n => n.Name), "Id", "Name");

            var transactions = db.Transactions.Include(t => t.Account1).Include(t => t.Vendor1).OrderByDescending(n => n.TransactionDate);
            return View(transactions.ToList().OrderByDescending(n => n.TransactionDate));
        }

        [HttpPost]
        public ActionResult Index(int? Vendor, int? Account)
        {
            ViewBag.Account = new SelectList(db.Accounts.OrderBy(n => n.Name), "Id", "Name");
            ViewBag.Vendor = new SelectList(db.Vendors.OrderBy(n => n.Name), "Id", "Name");

            var transactions = db.Transactions.Include(t => t.Account1).Include(t => t.Vendor1).OrderByDescending(n => n.TransactionDate);

            IOrderedQueryable<Transaction> transactions2;


            if(Vendor == null)
            {
                transactions2 = transactions;
            }
            else
            {
                transactions2 = transactions.Where(n => n.Vendor == Vendor).OrderByDescending(n => n.TransactionDate);
            }

            if (Account == null)
            {
            }
            else
            {
                transactions2 = transactions2.Where(n => n.Account == Account).OrderByDescending(n => n.TransactionDate);
            }

            return View(transactions2.ToList().OrderByDescending(n => n.TransactionDate));
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
            ViewBag.newVendorCategory = new SelectList(db.Categories.OrderBy(n => n.Name), "Id", "Name");

            ViewBag.Account = new SelectList(db.Accounts.OrderBy(n => n.Name), "Id", "Name");
            ViewBag.Vendor = new SelectList(db.Vendors.OrderBy(n => n.Name), "Id", "Name");
            return View();
        }

        // POST: /Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Description,TransactionDate,Vendor,Total,Account")] Transaction transaction, string newVendorName = "", int newVendorCategory = -1)
        {
            var newVendor = new Vendor();



            if (ModelState.IsValid)
            {

                if (newVendorCategory != -1)
                {
                    newVendor.Name = newVendorName;
                    newVendor.Category = newVendorCategory;
                    //db.Vendors.Add(newVendor);
                    transaction.Vendor = db.Vendors.Add(newVendor).Id;

                }
  
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Details",new {Id = transaction.Id});

            }

            ViewBag.Account = new SelectList(db.Accounts.OrderBy(n => n.Name), "Id", "Name", transaction.Account);
            ViewBag.Vendor = new SelectList(db.Vendors.OrderBy(n => n.Name), "Id", "Name", transaction.Vendor);
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
            ViewBag.Account = new SelectList(db.Accounts.OrderBy(n => n.Name), "Id", "Name", transaction.Account);
            ViewBag.Vendor = new SelectList(db.Vendors.OrderBy(n => n.Name), "Id", "Name", transaction.Vendor);
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
            ViewBag.Account = new SelectList(db.Accounts.OrderBy(n => n.Name), "Id", "Name", transaction.Account);
            ViewBag.Vendor = new SelectList(db.Vendors.OrderBy(n => n.Name), "Id", "Name", transaction.Vendor);
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
