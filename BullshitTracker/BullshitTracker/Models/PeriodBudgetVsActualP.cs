using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BullshitTracker.Models
{
    [MetadataType(typeof(PeriodBudgetVsActualMetaData))]
    public partial class PeriodBudgetVsActual
    {
    }

    public partial class PeriodBudgetVsActualMetaData
    {
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public double ActualAmount { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public Nullable<decimal> BudgetedAmount { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,0}")]
        public Nullable<double> PercentOfBudget { get; set; }
    }
}