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
    
    public partial class Cart_OrderDetail
    {
        public string cartID { get; set; }
        public string mbrID { get; set; }
        public string spcID { get; set; }
        public string odrID { get; set; }
        public int Quantity { get; set; }
        public Nullable<int> Discount { get; set; }
        public int usedPoints { get; set; }
        public string shpID { get; set; }
    
        public virtual Member Member { get; set; }
        public virtual Shipper Shipper { get; set; }
        public virtual Specification Specification { get; set; }
        public virtual Cart_OrderDetail Cart_OrderDetail1 { get; set; }
        public virtual Cart_OrderDetail Cart_OrderDetail2 { get; set; }
    }
}
