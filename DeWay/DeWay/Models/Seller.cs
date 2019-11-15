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
    
    public partial class Seller
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Seller()
        {
            this.FavoSeller = new HashSet<FavoSeller>();
            this.Product = new HashSet<Product>();
            this.SellerPhone = new HashSet<SellerPhone>();
        }
    
        public string selID { get; set; }
        public string selCompany { get; set; }
        public string selCity { get; set; }
        public string selDist { get; set; }
        public string selAddress { get; set; }
        public string GUINumber { get; set; }
        public string IDNumber { get; set; }
        public string selInfo { get; set; }
        public string selAut { get; set; }
        public string mbrID { get; set; }
        public string selImage { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FavoSeller> FavoSeller { get; set; }
        public virtual Member Member { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Product { get; set; }
        public virtual SellerAut SellerAut { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SellerPhone> SellerPhone { get; set; }
    }
}
