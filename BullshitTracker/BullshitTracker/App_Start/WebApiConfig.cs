using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using BullshitTracker.Models;

namespace BullshitTracker
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services



            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            
                
                );

            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Transaction>("Trans");
            builder.EntitySet<Account>("Accounts");
           // builder.EntitySet<ExceptionTransaction>("ExceptionTransaction");
            builder.EntitySet<Vendor>("Vendors");
            builder.EntitySet<PurchasesByCategory>("PurchasesByCategory");
            builder.EntitySet<ExceptionTransaction>("ExceptTrans");
            builder.EntitySet<Category>("CategoryAPI");



            //builder.EntitySet<Transaction>("Transactions");
            builder.EntitySet<TaxRate>("TaxRates");

            builder.EntitySet<Budget>("Budgets");



            config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());

        }
    }
}
