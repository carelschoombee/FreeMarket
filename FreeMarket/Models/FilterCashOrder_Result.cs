//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FreeMarket.Models
{
    using System;
    
    public partial class FilterCashOrder_Result
    {
        public Nullable<int> Id { get; set; }
        public Nullable<System.DateTime> DatePlaced { get; set; }
        public string Name { get; set; }
        public string DeliveryAddress { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<int> OrderId { get; set; }
        public Nullable<int> CashCustomerId { get; set; }
        public string Status { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<bool> Delivered { get; set; }
        public Nullable<bool> PaymentReceived { get; set; }
        public Nullable<bool> CashTransaction { get; set; }
        public Nullable<bool> BankTransfer { get; set; }
        public Nullable<bool> InvoiceSent { get; set; }
        public Nullable<decimal> ShippingTotal { get; set; }
        public string ContactName { get; set; }
    }
}
