using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace BullshitTracker.Models
{
    [MetadataType(typeof(TransactionMetaData))]
    public partial class Transaction
    {
    }

    public partial class TransactionMetaData
    {

        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public Nullable<decimal> Total { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}",ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> TransactionDate { get; set; }

    }
}