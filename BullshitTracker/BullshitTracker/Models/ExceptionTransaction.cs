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
    
    public partial class ExceptionTransaction
    {
        public int Id { get; set; }
        public Nullable<int> TransactionId { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> AmountPreTax { get; set; }
        public Nullable<bool> HST { get; set; }
        public Nullable<int> Category { get; set; }
        public bool Shame { get; set; }
        public Nullable<int> TaxRate { get; set; }
    
        public virtual Category Category1 { get; set; }
        public virtual Transaction Transaction { get; set; }
        public virtual TaxRate TaxRate1 { get; set; }
    }
}
