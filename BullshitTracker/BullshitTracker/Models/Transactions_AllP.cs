using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BullshitTracker.Models
{
    [MetadataType(typeof(Transactions_AllMetaData))]
    public partial class Transactions_All
    {
    }

    public partial class Transactions_AllMetaData
    {
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> TransactionDate { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public Nullable<double> ActualAmount { get; set; }


    }
}