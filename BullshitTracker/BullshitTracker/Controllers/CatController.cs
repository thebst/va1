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
    builder.EntitySet<Category>("Cat");
    builder.EntitySet<ExceptionTransaction>("ExceptionTransaction"); 
    builder.EntitySet<Vendor>("Vendor"); 
    builder.EntitySet<PurchasesByCategory>("PurchasesByCategory"); 
    builder.EntitySet<Budget>("Budgets"); 
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize(Users = @"MarkDeganiLocalhost, MarkDegani, RebeccaDegani")]
    public class CatController : ODataController
    {

        private BullshitTrackerEntities db = new BullshitTrackerEntities();

        // GET odata/Cat
        [Queryable]
        public IQueryable<Category> GetCat()
        {
            return db.Categories;
        }

        // GET odata/Cat(5)
        [Queryable]
        public SingleResult<Category> GetCategory([FromODataUri] int key)
        {
            return SingleResult.Create(db.Categories.Where(category => category.Id == key));
        }

        // PUT odata/Cat(5)
        public IHttpActionResult Put([FromODataUri] int key, Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != category.Id)
            {
                return BadRequest();
            }

            db.Entry(category).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(category);
        }

        // POST odata/Cat
        public IHttpActionResult Post(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categories.Add(category);
            db.SaveChanges();

            return Created(category);
        }

        // PATCH odata/Cat(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Category> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Category category = db.Categories.Find(key);
            if (category == null)
            {
                return NotFound();
            }

            patch.Patch(category);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(category);
        }

        // DELETE odata/Cat(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Category category = db.Categories.Find(key);
            if (category == null)
            {
                return NotFound();
            }

            db.Categories.Remove(category);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET odata/Cat(5)/ExceptionTransactions
        [Queryable]
        public IQueryable<ExceptionTransaction> GetExceptionTransactions([FromODataUri] int key)
        {
            return db.Categories.Where(m => m.Id == key).SelectMany(m => m.ExceptionTransactions);
        }

        // GET odata/Cat(5)/Vendors
        [Queryable]
        public IQueryable<Vendor> GetVendors([FromODataUri] int key)
        {
            return db.Categories.Where(m => m.Id == key).SelectMany(m => m.Vendors);
        }

        // GET odata/Cat(5)/PurchasesByCategories
        [Queryable]
        public IQueryable<PurchasesByCategory> GetPurchasesByCategories([FromODataUri] int key)
        {
            return db.Categories.Where(m => m.Id == key).SelectMany(m => m.PurchasesByCategories);
        }

        // GET odata/Cat(5)/Budget1
        [Queryable]
        public SingleResult<Budget> GetBudget1([FromODataUri] int key)
        {
            return SingleResult.Create(db.Categories.Where(m => m.Id == key).Select(m => m.Budget1));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int key)
        {
            return db.Categories.Count(e => e.Id == key) > 0;
        }
    }
}
