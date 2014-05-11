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
    builder.EntitySet<ExceptionTransaction>("ExceptTrans");
    builder.EntitySet<Category>("Categories"); 
    builder.EntitySet<Transaction>("Transactions"); 
    builder.EntitySet<TaxRate>("TaxRates"); 
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize(Users = @"MarkDeganiLocalhost, MarkDegani, RebeccaDegani")]
    public class ExceptTransController : ODataController
    {
        private BullshitTrackerEntities db = new BullshitTrackerEntities();

        // GET odata/ExceptTrans
        [Queryable]
        public IQueryable<ExceptionTransaction> GetExceptTrans()
        {
            return db.ExceptionTransactions;
        }

        // GET odata/ExceptTrans(5)
        [Queryable]
        public SingleResult<ExceptionTransaction> GetExceptionTransaction([FromODataUri] int key)
        {
            return SingleResult.Create(db.ExceptionTransactions.Where(exceptiontransaction => exceptiontransaction.Id == key));
        }

        // PUT odata/ExceptTrans(5)
        public IHttpActionResult Put([FromODataUri] int key, ExceptionTransaction exceptiontransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != exceptiontransaction.Id)
            {
                return BadRequest();
            }

            db.Entry(exceptiontransaction).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExceptionTransactionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(exceptiontransaction);
        }

        // POST odata/ExceptTrans
        public IHttpActionResult Post(ExceptionTransaction exceptiontransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ExceptionTransactions.Add(exceptiontransaction);
            db.SaveChanges();

            return Created(exceptiontransaction);
        }

        // PATCH odata/ExceptTrans(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<ExceptionTransaction> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ExceptionTransaction exceptiontransaction = db.ExceptionTransactions.Find(key);
            if (exceptiontransaction == null)
            {
                return NotFound();
            }

            patch.Patch(exceptiontransaction);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExceptionTransactionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(exceptiontransaction);
        }

        // DELETE odata/ExceptTrans(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            ExceptionTransaction exceptiontransaction = db.ExceptionTransactions.Find(key);
            if (exceptiontransaction == null)
            {
                return NotFound();
            }

            db.ExceptionTransactions.Remove(exceptiontransaction);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET odata/ExceptTrans(5)/Category1
        [Queryable]
        public SingleResult<Category> GetCategory1([FromODataUri] int key)
        {
            return SingleResult.Create(db.ExceptionTransactions.Where(m => m.Id == key).Select(m => m.Category1));
        }

        // GET odata/ExceptTrans(5)/Transaction
        [Queryable]
        public SingleResult<Transaction> GetTransaction([FromODataUri] int key)
        {
            return SingleResult.Create(db.ExceptionTransactions.Where(m => m.Id == key).Select(m => m.Transaction));
        }

        // GET odata/ExceptTrans(5)/TaxRate1
        [Queryable]
        public SingleResult<TaxRate> GetTaxRate1([FromODataUri] int key)
        {
            return SingleResult.Create(db.ExceptionTransactions.Where(m => m.Id == key).Select(m => m.TaxRate1));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExceptionTransactionExists(int key)
        {
            return db.ExceptionTransactions.Count(e => e.Id == key) > 0;
        }
    }
}
