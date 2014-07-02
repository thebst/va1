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
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public Nullable<decimal> Total { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> TransactionDate { get; set; }

    }
}