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
    public class ExpenseDetailsController : Controller
    {
        private BullshitTrackerEntities db = new BullshitTrackerEntities();

        public ActionResult Index(int? Category, int? Budget, int? Period)
        {
            ViewBag.Category = new SelectList(db.Categories.OrderBy(n => n.Name).Where(n => n.Budget == Budget), "Id", "Name");
            ViewBag.Budget = new SelectList(db.Budgets.OrderBy(n => n.BudgetName), "Id", "BudgetName");
            ViewBag.Period = new SelectList(db.Periods.OrderBy(n => n.StartDate), "Id", "Name");


            var transactions_All = db.Transactions_All.Include(t => t.Transaction).OrderByDescending(n => n.TransactionId);

            IOrderedQueryable<Transactions_All> transactions_All2;


            if (Category == null  || Category == -1)
            {
                transactions_All2 = transactions_All;
            }
            else
            {
                transactions_All2 = transactions_All.Where(n => n.CategoryId == Category).OrderByDescending(n => n.TransactionId);
            }

            if (Budget == null)
            {
            }
            else
            {
                transactions_All2 = transactions_All2.Where(n => n.BudgetId == Budget).OrderByDescending(n => n.TransactionDate);
            }

            if (Period == null) 
            {
            }
            else
            {
                transactions_All2 = transactions_All2.Where(n => n.PeriodId == Period).OrderByDescending(n => n.TransactionDate);
            }

            return View(transactions_All2.ToList().OrderByDescending(n => n.TransactionDate));
        }

        public ActionResult Categoryxs(int Budget)
        {
            return Json( new {result = (from obj in db.Categories select new {Id= obj.Id, Name= obj.Name, Budget = obj.Budget}).Where(x => x.Budget == Budget)}, JsonRequestBehavior.AllowGet);
        }

        // GET: ExpenseDetails/Details/5ws
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transactions_All transactions_All = db.Transactions_All.Find(id);
            if (transactions_All == null)
            {
                return HttpNotFound();
            }
            return View(transactions_All);
        }

    }
}
