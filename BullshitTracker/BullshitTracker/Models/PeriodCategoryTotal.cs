//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BullshitTracker.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PeriodCategoryTotal
    {
        public int BudgetId { get; set; }
        public int PeriodId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public double ActualAmount { get; set; }
        public Nullable<decimal> BudgetAmount { get; set; }
        public Nullable<double> PercentOfBudget { get; set; }
    
        public virtual PeriodBudgetVsActual PeriodBudgetVsActual { get; set; }
    }
}
