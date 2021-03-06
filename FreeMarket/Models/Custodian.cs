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
    
    public partial class Custodian
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Custodian()
        {
            this.CourierStockMovementLogs = new HashSet<CourierStockMovementLog>();
            this.ProductCustodians = new HashSet<ProductCustodian>();
        }
    
        public int CustodianNumber { get; set; }
        public Nullable<int> LocationNumber { get; set; }
        public string CustodianName { get; set; }
        public string CustodianTelephoneNumber { get; set; }
        public string CustodianCellphoneNumber { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourierStockMovementLog> CourierStockMovementLogs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductCustodian> ProductCustodians { get; set; }
    }
}
