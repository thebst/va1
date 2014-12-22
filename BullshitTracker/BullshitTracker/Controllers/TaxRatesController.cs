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
    public class TaxRatesController : Controller
    {
        private BullshitTrackerEntities db = new BullshitTrackerEntities();

        // GET: /TaxRates/
        public ActionResult Index()
        {
            return View(db.TaxRates.ToList());
        }

        // GET: /TaxRates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxRate taxrate = db.TaxRates.Find(id);
            if (taxrate == null)
            {
                return HttpNotFound();
            }
            return View(taxrate);
        }

        // GET: /TaxRates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TaxRates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Rate,Description")] TaxRate taxrate)
        {
            if (ModelState.IsValid)
            {
                db.TaxRates.Add(taxrate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taxrate);
        }

        // GET: /TaxRates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxRate taxrate = db.TaxRates.Find(id);
            if (taxrate == null)
            {
                return HttpNotFound();
            }
            return View(taxrate);
        }

        // POST: /TaxRates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Rate,Description")] TaxRate taxrate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taxrate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taxrate);
        }

        // GET: /TaxRates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxRate taxrate = db.TaxRates.Find(id);
            if (taxrate == null)
            {
                return HttpNotFound();
            }
            return View(taxrate);
        }

        // POST: /TaxRates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaxRate taxrate = db.TaxRates.Find(id);
            db.TaxRates.Remove(taxrate);
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
