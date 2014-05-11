using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using BullshitTracker.Models;

namespace BullshitTracker.Controllers
{
    /*
    To add a route for this controller, merge these statements into the Register method of the WebApiConfig class. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using BullshitTracker.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Transaction>("Trans");
    builder.EntitySet<Account>("Accounts"); 
    builder.EntitySet<ExceptionTransaction>("ExceptionTransaction"); 
    builder.EntitySet<Vendor>("Vendors"); 
    builder.EntitySet<PurchasesByCategory>("PurchasesByCategory"); 
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize(Users = @"MarkDeganiLocalhost, MarkDegani, RebeccaDegani")]
    public class TransController : ODataController
    {
        private BullshitTrackerEntities db = new BullshitTrackerEntities();

        // GET odata/Trans
        [Queryable]
        public IQueryable<Transaction> GetTrans()
        {
            return db.Transactions;
        }

        // GET odata/Trans(5)
        [Queryable]
        public SingleResult<Transaction> GetTransaction([FromODataUri] int key)
        {
            return SingleResult.Create(db.Transactions.Where(transaction => transaction.Id == key));
        }

        // PUT odata/Trans(5)
        public IHttpActionResult Put([FromODataUri] int key, Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != transaction.Id)
            {
                return BadRequest();
            }

            db.Entry(transaction).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(transaction);
        }

        // POST odata/Trans
        public IHttpActionResult Post(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Transactions.Add(transaction);
            db.SaveChanges();

            return Created(transaction);
        }

        // PATCH odata/Trans(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Transaction> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Transaction transaction = db.Transactions.Find(key);
            if (transaction == null)
            {
                return NotFound();
            }

            patch.Patch(transaction);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(transaction);
        }

        // DELETE odata/Trans(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Transaction transaction = db.Transactions.Find(key);
            if (transaction == null)
            {
                return NotFound();
            }

            db.Transactions.Remove(transaction);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET odata/Trans(5)/Account1
        [Queryable]
        public SingleResult<Account> GetAccount1([FromODataUri] int key)
        {
            return SingleResult.Create(db.Transactions.Where(m => m.Id == key).Select(m => m.Account1));
        }

        // GET odata/Trans(5)/ExceptionTransactions
        [Queryable]
        public IQueryable<ExceptionTransaction> GetExceptionTransactions([FromODataUri] int key)
        {
            return db.Transactions.Where(m => m.Id == key).SelectMany(m => m.ExceptionTransactions);
        }

        // GET odata/Trans(5)/Vendor1
        [Queryable]
        public SingleResult<Vendor> GetVendor1([FromODataUri] int key)
        {
            return SingleResult.Create(db.Transactions.Where(m => m.Id == key).Select(m => m.Vendor1));
        }

        // GET odata/Trans(5)/PurchasesByCategories
        [Queryable]
        public IQueryable<PurchasesByCategory> GetPurchasesByCategories([FromODataUri] int key)
        {
            return db.Transactions.Where(m => m.Id == key).SelectMany(m => m.PurchasesByCategories);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TransactionExists(int key)
        {
            return db.Transactions.Count(e => e.Id == key) > 0;
        }
    }
}
