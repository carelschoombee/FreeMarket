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
    using System.Collections.Generic;
    
    public partial class Supplier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supplier()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
            this.SupplierAddresses = new HashSet<SupplierAddress>();
            this.ProductSuppliers = new HashSet<ProductSupplier>();
        }
    
        public int SupplierNumber { get; set; }
        public string SupplierName { get; set; }
        public string MainContactName { get; set; }
        public string MainContactTelephoneNumber { get; set; }
        public string MainContactCellphoneNumber { get; set; }
        public string MainContactEmailAddress { get; set; }
        public string BankingDetailsBankName { get; set; }
        public string BankingDetailsBranchName { get; set; }
        public string BankingDetailsBranchCode { get; set; }
        public string BankingDetailsAccountNumber { get; set; }
        public string BankingDetailsAccountType { get; set; }
        public Nullable<System.DateTime> DateAdded { get; set; }
        public bool TrustedSupplier { get; set; }
        public bool Activated { get; set; }
        public string UserId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplierAddress> SupplierAddresses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSupplier> ProductSuppliers { get; set; }
    }
}
