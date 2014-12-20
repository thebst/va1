using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BullshitTracker.Models
{

    [MetadataType(typeof(PeriodCategoryTotalMetaData))]
    public partial class PeriodCategoryTotal
    {
    }

    public partial class PeriodCategoryTotalMetaData
    {
        public int BudgetId { get; set; }
        public int PeriodId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public double ActualAmount { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public double BudgetAmount { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,0}")]
        public double PercentOfBudget {get;set;}


        public virtual PeriodBudgetVsActual PeriodBudgetVsActual { get; set; }
    }
}