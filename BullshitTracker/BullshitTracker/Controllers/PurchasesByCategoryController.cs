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
    public class PurchasesByCategoryController : Controller
    {
        private BullshitTrackerEntities db = new BullshitTrackerEntities();

        // GET: /PurchasesByCategory/
        public ActionResult Index()
        {
            var purchasesbycategories = db.PurchasesByCategories.Include(p => p.Category).Include(p => p.Transaction);
            return View(purchasesbycategories.ToList());
        }

        // GET: /PurchasesByCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchasesByCategory purchasesbycategory = db.PurchasesByCategories.Find(id);
            if (purchasesbycategory == null)
            {
                return HttpNotFound();
            }
            return View(purchasesbycategory);
        }

        // GET: /PurchasesByCategory/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.TransactionId = new SelectList(db.Transactions, "Id", "Description");
            return View();
        }

        // POST: /PurchasesByCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TransactionId,CategoryId,Description,Amount")] PurchasesByCategory purchasesbycategory)
        {
            if (ModelState.IsValid)
            {
                db.PurchasesByCategories.Add(purchasesbycategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", purchasesbycategory.CategoryId);
            ViewBag.TransactionId = new SelectList(db.Transactions, "Id", "Description", purchasesbycategory.TransactionId);
            return View(purchasesbycategory);
        }

        // GET: /PurchasesByCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchasesByCategory purchasesbycategory = db.PurchasesByCategories.Find(id);
            if (purchasesbycategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", purchasesbycategory.CategoryId);
            ViewBag.TransactionId = new SelectList(db.Transactions, "Id", "Description", purchasesbycategory.TransactionId);
            return View(purchasesbycategory);
        }

        // POST: /PurchasesByCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TransactionId,CategoryId,Description,Amount")] PurchasesByCategory purchasesbycategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchasesbycategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", purchasesbycategory.CategoryId);
            ViewBag.TransactionId = new SelectList(db.Transactions, "Id", "Description", purchasesbycategory.TransactionId);
            return View(purchasesbycategory);
        }

        // GET: /PurchasesByCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchasesByCategory purchasesbycategory = db.PurchasesByCategories.Find(id);
            if (purchasesbycategory == null)
            {
                return HttpNotFound();
            }
            return View(purchasesbycategory);
        }

        // POST: /PurchasesByCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchasesByCategory purchasesbycategory = db.PurchasesByCategories.Find(id);
            db.PurchasesByCategories.Remove(purchasesbycategory);
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
