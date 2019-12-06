//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DeWay.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DeWay.Models.Metadata;


    [MetadataType(typeof(MetaDataOrder))]

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.Refund = new HashSet<Refund>();
            this.Review = new HashSet<Review>();
        }
    
        public string odrID { get; set; }
        public string pmtID { get; set; }
        public string odrStatusID { get; set; }
        public string recName { get; set; }
        public string recCity { get; set; }
        public string recDist { get; set; }
        public string recAddress { get; set; }
        public string recPhone { get; set; }
        public Nullable<System.DateTime> pmtDate { get; set; }
        public System.DateTime odrDate { get; set; }
        public Nullable<System.DateTime> shpDate { get; set; }
        public string odrNote { get; set; }
        public string traceNumber { get; set; }
        public string cashFlowID { get; set; }
        public string selID { get; set; }
    
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Refund> Refund { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Review { get; set; }
        public virtual Seller Seller { get; set; }
    }
}
