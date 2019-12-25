using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeWay.Models
{
    public class Metadata
    {
        public class MetadataactBulletin
        {
            [Key]
            [DisplayName("公告編號")]
            public string actID { get; set; }


            [DisplayName("商品編號")]
            public string pdtID { get; set; }


            [DisplayName("公告起始時間")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> actStrDate { get; set; }


            [DisplayName("公告結束時間")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> actEndDate { get; set; }


            [DisplayName("活動照片")]
            [Required(ErrorMessage = "此欄位為必填")]
            public string actImage { get; set; }


            [DisplayName("是否顯示")]
            [Required(ErrorMessage = "此欄位為必填")]
            public bool actDisplay { get; set; }


            [DisplayName("管理員編號")]
            [Required(ErrorMessage = "此欄位為必填")]
            public string admID { get; set; }


        }

        public class MetadataAdm
        {
            [Key]
            [DisplayName("管理員編號")]
            public string admID { get; set; }


            [DisplayName("管理員姓名")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(20, ErrorMessage = "最多20個字")]
            public string admName { get; set; }


            [DisplayName("管理員信箱")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(64, ErrorMessage = "此欄位最長為64字")]
            [DataType(DataType.EmailAddress, ErrorMessage = "請輸入正確的Email格式!")]
            [EmailAddress]
            public string admMail { get; set; }


            [DisplayName("管理員電話")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(20, ErrorMessage = "最多20碼")]
            [RegularExpression(@"0[0-9]{3}-[0-9]{3}-[0-9]{3}|\([0-9]{2,3}\)[0-9]{3}-[0-9]{3,4}", ErrorMessage = "請輸入正確格式：xxxx-xxx-xxx |(xx)xxx-xxxx")]
            public string admPhone { get; set; }


        }

        public class MetadataAdmAccount
        {

            [DisplayName("管理員帳號")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(20, ErrorMessage = "最多為20碼")]
            public string admAct { get; set; }

            [DisplayName("管理員密碼")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(20, ErrorMessage = "最多為20碼")]
            public string admPwd { get; set; }

         

            [Key]
            [DisplayName("管理員編號")]
            public string admID { get; set; }

        }

        public class MetadataCart_OrderDetail
        {
            [Key]
            [DisplayName("購物車編號")]

            public string cartID { get; set; }

            [DisplayName("會員編號")]
            public string mbrID { get; set; }

            [DisplayName("商品規格編號")]
            public string spcID { get; set; }

            [DisplayName("訂單編號")]
            public string odrID { get; set; }

            [DisplayName("數量")]
            [Required(ErrorMessage = "此欄位為必填")]
            public int Quantity { get; set; }

            [DisplayName("折扣")]
            public Nullable<int> Discount { get; set; }

            [DisplayName("已使用點數")]
            [Required(ErrorMessage = "此欄位為必填")]
            public int usedPoints { get; set; }

            [DisplayName("物流編號")]
            [Required(ErrorMessage = "此欄位為必填")]
            public string shpID { get; set; }

            [DisplayName("價錢")]
            [Required(ErrorMessage = "此欄位為必填")]
            public Nullable<decimal> pdtPrice { get; set; }
        }

        public class MetadataFavoSeller
        {

         


            [DisplayName("賣家編號")]
            public string selID { get; set; }


            [DisplayName("會員編號")]
            public string mbrID { get; set; }


            [Key]
            [DisplayName("最愛賣家編號")]
            public string favoID { get; set; }

        }

        public class MetadataFirstLayer
        {
            [Key]
            [DisplayName("第一層分類編號")]
            public string fstLayerID { get; set; }

            [DisplayName("第一層分類")]
            [StringLength(15, ErrorMessage = "最多15碼")]
            public string fstLayer { get; set; }


        }

        public class MetadataMember
        {
            [Key]
            [DisplayName("會員編號")]
            public string mbrID { get; set; }

            [DisplayName("姓名")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(20, ErrorMessage = "此欄位最長為20字")]
            public string mbrName { get; set; }


            [DisplayName("暱稱")]
            [StringLength(20, ErrorMessage = "此欄位最長為20字")]
            public string nickName { get; set; }

            [DisplayName("電話")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(20, ErrorMessage = "此欄位最長為20字")]
            [RegularExpression(@"0[0-9]{3}-[0-9]{3}-[0-9]{3}|\([0-9]{2,3}\)[0-9]{3}-[0-9]{3,4}", ErrorMessage = "請輸入正確格式：xxxx-xxx-xxx |(xx)xxx-xxxx")]
            public string mbrPhone { get; set; }

            [DisplayName("信箱")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(64, ErrorMessage = "此欄位最長為64字")]
            [DataType(DataType.EmailAddress, ErrorMessage = "請輸入正確的信箱格式!")]
            [EmailAddress]
            public string mbrMail { get; set; }

            [DisplayName("生日")]
            [DataType(DataType.Date, ErrorMessage = "輸入日期有誤")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> birthDate { get; set; }

            [DisplayName("我的點數")]
            public short Points { get; set; }

            [DisplayName("會員認證")]
            public bool mbrAut { get; set; }

            [DisplayName("註冊日期")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public System.DateTime signupDate { get; set; }

            [DisplayName("大頭照")]
            public string mbrImage { get; set; }
            [DisplayName("是否封鎖")]
            public Nullable<bool> mbrBlock { get; set; }


        }

        public class MetadataMemberAccount
        {

            [DisplayName("會員帳號")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(20, MinimumLength = 8, ErrorMessage = "請輸入8~20碼的英文和數字!")]
            public string mbrAccount { get; set; }


            [DataType(DataType.Password)]
            [DisplayName("會員密碼")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(20, MinimumLength = 8, ErrorMessage = "請輸入8~20碼的英文和數字,且第一個字母需為大寫!")]
            [RegularExpression(@"[A-Z][a-zA-Z0-9]{7,}", ErrorMessage = "請輸入8~20碼的英文和數字，且第一個字母需為大寫!")]
            public string mbrPwd { get; set; }

            [DisplayName("會員編號")]
            public string mbrID { get; set; }

        }

        public class MetadataMessage
        {
            [Key]
            [DisplayName("訊息編號")]
            public string msgID { get; set; }

          

            [StringLength(120, ErrorMessage = "此欄位最長為120字")]
            public string msgContent { get; set; }

            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}")]
            public Nullable<System.DateTime> msgTime { get; set; }

            public bool msgStatus { get; set; }


        }

        public class MetadatantfCategory
        {
            [Key]
            [DisplayName("通知種類編號")]
            public string ntfCtgID { get; set; }

            [DisplayName("通知種類")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(10, ErrorMessage = "此欄位最長為10字")]
            public string ntfType { get; set; }


        }
        public class MetadatantfRecord
        {
            [DisplayName("通知內容")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(120, ErrorMessage = "此欄位最長為10字")]
            public string ntfContent { get; set; }

            [DisplayName("通知種類編號")]
            [Required(ErrorMessage = "此欄位為必填")]
            public string ntfCtgID { get; set; }

            [DisplayName("會員編號")]
            [Required(ErrorMessage = "此欄位為必填")]
            public string mbrID { get; set; }

            [DisplayName("通知標題")]
            [StringLength(30, ErrorMessage = "此欄位最長為30字")]
            public string ntfTitle { get; set; }

            [DisplayName("通知日期")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public System.DateTime ntfTime { get; set; }


            [Key]
            [DisplayName("通知編號")]
            public string ntfID { get; set; }

        }
        public class MetadataOrder
        {
            [DisplayName("訂單編號")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(10, ErrorMessage = "此欄位最長為10碼")]
            public string odrID { get; set; }
            [DisplayName("付款方式")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(10, ErrorMessage = "此欄位最長為10碼")]
            public string pmtID { get; set; }
            [DisplayName("訂單狀態編號")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(10, ErrorMessage = "此欄位最長為10碼")]
            public string odrStatusID { get; set; }
            [DisplayName("收件人姓名")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(20, ErrorMessage = "此欄位最長為20字")]
            public string recName { get; set; }
            [DisplayName("市")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(3, ErrorMessage = "此欄位最長為3字")]
            public string recCity { get; set; }
            [DisplayName("區")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(3, ErrorMessage = "此欄位最長為3字")]
            public string recDist { get; set; }
            [DisplayName("地址")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(20, ErrorMessage = "此欄位最長為20字")]
            public string recAddress { get; set; }
            [DisplayName("收件人電話")]
            [Required(ErrorMessage = "此欄位為必填")]
            [RegularExpression(@"0[0-9]{3}-[0-9]{3}-[0-9]{3}|\([0-9]{2,3}\)[0-9]{3}-[0-9]{3,4}", ErrorMessage = "請輸入正確格式：xxxx-xxx-xxx ")]
            [StringLength(20, ErrorMessage = "此欄位最長為20碼")]
            public string recPhone { get; set; }

            [DisplayName("付款日期")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> pmtDate { get; set; }
            [DisplayName("訂單日期")]
            [Required(ErrorMessage = "此欄位為必填")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public System.DateTime odrDate { get; set; }
            [DisplayName("出貨日期")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

            public Nullable<System.DateTime> shpDate { get; set; }
            [DisplayName("訂單備註")]

            [StringLength(120, ErrorMessage = "此欄位最長為120字")]
            public string odrNote { get; set; }
            [DisplayName("託運單號")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(20, ErrorMessage = "此欄位最長為20碼")]
            public string traceNumber { get; set; }
            [DisplayName("第三方金流代號")]

            [StringLength(25, ErrorMessage = "此欄位最長為25碼")]
            public string cashFlowID { get; set; }

            [DisplayName("賣家編號")]
            [Required(ErrorMessage = "此欄位為必填")]
            public string selID { get; set; }

            [DisplayName("運費")]
            [Required(ErrorMessage = "此欄位為必填")]
            public Nullable<decimal> shpPrice { get; set; }
        }

        public class MetadataOrderStatus
        {
            [DisplayName("訂單狀態編號")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(10, ErrorMessage = "此欄位最長為10碼")]
            public string odrStatusID { get; set; }

            [DisplayName("訂單狀態")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(10, ErrorMessage = "此欄位最長為10碼")]
            public string odrStatus { get; set; }
        }

        public class MetadataPaymentMethod
        {
            [DisplayName("付款方式編號")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(10, ErrorMessage = "此欄位最長為10碼")]
            public string pmtID { get; set; }
            [DisplayName("付款方式")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(10, ErrorMessage = "此欄位最長為10碼")]
            public string pmtMethod { get; set; }
        }

        public class MetadataProduct
        {
            [DisplayName("商品編號")]
            [StringLength(10, ErrorMessage = "此欄位最長為10碼")]
            public string pdtID { get; set; }
            [DisplayName("賣家編號")]
            [StringLength(10, ErrorMessage = "此欄位最長為10碼")]
            public string selID { get; set; }
            [DisplayName("商品名稱")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(50, ErrorMessage = "此欄位最長為50字")]
            public string pdtName { get; set; }
            [DisplayName("上架日期")]
            [Required(ErrorMessage = "此欄位為必填")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public System.DateTime pdtDate { get; set; }
            [DisplayName("商品描述")]
            [StringLength(120, ErrorMessage = "此欄位最長為120字")]
            public string pdtDescribe { get; set; }
            [DisplayName("是否上架")]
            [Required(ErrorMessage = "此欄位為必填")]

            public bool Discontinued { get; set; }
            [DisplayName("種類編號")]
            [StringLength(10, ErrorMessage = "此欄位最長為10碼")]
            public string ctgID { get; set; }
        }

        public partial class MetadataProductCategory
        {
            [DisplayName("種類編號")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(10, ErrorMessage = "此欄位最長為10碼")]
            public string pdtCtgID { get; set; }
            [DisplayName("第一層分類")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(10, ErrorMessage = "此欄位最長為10碼")]
            public string fstLayerID { get; set; }
            [DisplayName("第二層分類")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(10, ErrorMessage = "此欄位最長為10碼")]
            public string sndLayerID { get; set; }
            [DisplayName("第三層分類")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(10, ErrorMessage = "此欄位最長為10碼")]
            public string trdLayerID { get; set; }
        }

        public partial class MetadataProductImage
        {
            [DisplayName("商品照片")]
            [Required(ErrorMessage = "此欄位為必填")]

            public string pdtImage { get; set; }
            [DisplayName("商品編號")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(10, ErrorMessage = "此欄位最長為10碼")]
            public string pdtID { get; set; }
            [DisplayName("商品照片編號")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(10, ErrorMessage = "此欄位最長為10碼")]
            public string pImgID { get; set; }
        }

        public partial class MetadataReview
        {
            [DisplayName("評論流水編號")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(10, ErrorMessage = "此欄位最長為10碼")]
            public string rvwID { get; set; }
            [DisplayName("內容")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(120, ErrorMessage = "此欄位最長為120字")]
            public string rvwContent { get; set; }
            [DisplayName("時間")]
            [Required(ErrorMessage = "此欄位為必填")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public System.DateTime rvwTime { get; set; }
            [DisplayName("星數")]
            [Required(ErrorMessage = "此欄位為必填")]
            
            public short rvwStar { get; set; }
            [DisplayName("評論者編號")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(10, ErrorMessage = "此欄位最長為10碼")]
            public string mbrID { get; set; }
            [DisplayName("商品編號")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(10, ErrorMessage = "此欄位最長為10碼")]
            public string pdtID { get; set; }
            [DisplayName("訂單編號")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(10, ErrorMessage = "此欄位最長為10碼")]
            public string odrID { get; set; }
        }

        public partial class MetadataRefundStatus
        {
            [DisplayName("退貨單狀態編號")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(10, ErrorMessage = "此欄位最長為10碼")]
            public string rfdStatusID { get; set; }
            [DisplayName("退貨單狀態")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(10, ErrorMessage = "此欄位最長為10碼")]
            public string rfdStatus { get; set; }
        }

        public partial class MetadataRefundAccount
        {
            [DisplayName("銀行帳號")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(20, ErrorMessage = "此欄位最長為20碼")]
            public string bankAccount { get; set; }
            [DisplayName("銀行名稱")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(25, ErrorMessage = "此欄位最長為25個字")]
            public string bankName { get; set; }
            [DisplayName("銀行代碼")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(20, ErrorMessage = "此欄位最長為20碼")]
            public string bankCode { get; set; }
            [DisplayName("退貨流水編號")]
            
            [StringLength(10, ErrorMessage = "此欄位最長為10碼")]
            public string rfdID { get; set; }
        }

        public partial class MetadataRefund
        {
            [DisplayName("退貨流水編號")]
            
            [StringLength(10, ErrorMessage = "此欄位最長為10碼")]
            public string rfdID { get; set; }
            [DisplayName("訂單編號")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(10, ErrorMessage = "此欄位最長為10碼")]
            public string odrID { get; set; }

            [DisplayName("退回商品的運費")]

            
            [DataType(DataType.Currency)]
            public Nullable<decimal> rfdShip { get; set; }
            [DisplayName("退貨原因")]
            [StringLength(120, ErrorMessage = "此欄位最長為120字")]
            public string rfdReason { get; set; }

            [DisplayName("是否退回商品")]
           
            public bool rfdProduct { get; set; }

            [DisplayName("賣家是否負擔退回商品的運費")]
            public bool rfdShipping { get; set; }

            [DisplayName("日期")]
            
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public System.DateTime rfdDate { get; set; }
            [DisplayName("退貨單狀態")]
            
            [StringLength(10, ErrorMessage = "此欄位最長為10碼")]
            public string rfdStatusID { get; set; }
        }

        public partial class MetadataQA
        {
            public string qaID { get; set; }
            [DisplayName("賣家已讀狀態")]
            public Nullable<bool> readStatus { get; set; }
            [DisplayName("發問者會員編號")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(10, ErrorMessage = "此欄位最長為10碼")]
            public string mbrID { get; set; }
            [DisplayName("提問內容")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(120, ErrorMessage = "此欄位最長為120字")]
            public string Question { get; set; }
            [DisplayName("回答內容")]
            [StringLength(120, ErrorMessage = "此欄位最長為120字")]
            public string Answer { get; set; }
            [DisplayName("時間")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> qaTime { get; set; }
            [DisplayName("顯示狀態")]
            public Nullable<bool> displayStatus { get; set; }
            [DisplayName("商品編號")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(10, ErrorMessage = "此欄位最長為10碼")]
            public string pdtID { get; set; }
        }



        public partial class MetadatarptRecord
        {
            [DisplayName("檢舉流水編號")]
            public string rptID { get; set; }
            [DisplayName("檢舉人編號")]
            public string mbrID { get; set; }
            [DisplayName("被檢舉物件編號")]
            public string objID { get; set; }
            [DisplayName("標題")]
            [StringLength(20, ErrorMessage = "此欄位最長為20字")]
            public string rptTitle { get; set; }
            [DisplayName("檢舉內容	rptContent")]
            [StringLength(120, ErrorMessage = "此欄位最長為120字")]
            public string rptContent { get; set; }
            [DisplayName("時間")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public System.DateTime rptTime { get; set; }
            [DisplayName("管理員編號")]
            public string admID { get; set; }
        }

        public partial class MetadataSecondLayer
        {
            [DisplayName("第二層分類編號")]
            public string sndLayerID { get; set; }
            [DisplayName("第二層分類名稱")]
            [StringLength(15, ErrorMessage = "此欄位最長為15字")]
            public string sndLayer { get; set; }
        }

        public partial class MetadataSeller
        {
            [DisplayName("賣家編號")]
            public string selID { get; set; }
            [DisplayName("賣家公司名稱")]
            [StringLength(20, ErrorMessage = "此欄位最長為20字")]
            public string selCompany { get; set; }
            [DisplayName("市")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(3, ErrorMessage = "此欄位最長為3字")]

            public string selCity { get; set; }
            [DisplayName("區")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(5, ErrorMessage = "此欄位最長為5字")]

            public string selDist { get; set; }
            [DisplayName("路(地址)")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(20, ErrorMessage = "此欄位最長為20字")]

            public string selAddress { get; set; }
            [DisplayName("統一編號")]
            [Required(ErrorMessage = "此欄位為必填")]
            public string GUINumber { get; set; }
            [RegularExpression(@"[A-Z][1-2][0-9]{8}", ErrorMessage = "請輸入正確格式")]
            [DisplayName("身份證字號")]
            [Required(ErrorMessage = "此欄位為必填")]
            public string IDNumber { get; set; }
            [DisplayName("賣場介紹")]
            [StringLength(500, ErrorMessage = "此欄位最長為500字")]
            public string selInfo { get; set; }
            [DisplayName("認證編號")]
            public string selAut { get; set; }
            [DisplayName("會員編號")]
            public string mbrID { get; set; }
            [DisplayName("賣家照片")]
            public string selImage { get; set; }
        }

        public partial class MetadataSellAut
        {
            [DisplayName("認證編號")]
            public string selAut { get; set; }
            [DisplayName("認證種類")]
            public string autCategory { get; set; }

        }

        public partial class MetadataSellPhone
        {
            [DisplayName("賣家電話")]
            [RegularExpression(@"0[0-9]{3}-[0-9]{3}-[0-9]{3}|\([0-9]{2,3}\)[0-9]{3}-[0-9]{3,4}", ErrorMessage = "請輸入正確格式：xxxx-xxx-xxx ")]
            public string selPhone { get; set; }
            [DisplayName("賣家編號")]
            public string selID { get; set; }
        }

        public partial class MetadataShipper
        {
            [DisplayName("物流編號")]
            public string shpID { get; set; }
            [DisplayName("物流方式")]
            [StringLength(10, ErrorMessage = "此欄位最長為10字")]
            public string shpMethod { get; set; }

        }

        public partial class MetadataShipperDetail
        {
            [DisplayName("商品編號")]
            public string shpID { get; set; }
            [DisplayName("物流編號")]
            public string pdtID { get; set; }
            [DisplayName("自訂運費")]
            [Required(ErrorMessage = "此欄位為必填")]
            public decimal defaultShipping { get; set; }
        }

        public partial class MetadataSpecification
        {
            [DisplayName("規格流水編號")]
            public string spcID { get; set; }
            [DisplayName("項目")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(20, ErrorMessage = "此欄位最長為20字")]
            public string Style { get; set; }
            [DisplayName("規格")]
            [StringLength(20, ErrorMessage = "此欄位最長為20字")]
            public string Size { get; set; }
            [DisplayName("商品庫存")]
            [Required(ErrorMessage = "此欄位為必填")]

            public string Stock { get; set; }
            [DisplayName("商品定價")]
            [Required(ErrorMessage = "此欄位為必填")]
            public Nullable<decimal> Price { get; set; }
            [DisplayName("商品編號")]
            public string pdtID { get; set; }

        }

        public partial class MetadataTag
        {
            [DisplayName("標籤流水編號")]
            public string tagID { get; set; }
            [DisplayName("標籤名稱")]
            [Required(ErrorMessage = "此欄位為必填")]
            [StringLength(20, ErrorMessage = "此欄位最長為20字")]
            public string tagName { get; set; }
            [DisplayName("商品編號")]
            public string pdtID { get; set; }
        }

        public partial class MetadataThirdLayer
        {
            [DisplayName("第三層分類編號")]
            public string trdLayerID { get; set; }
            [DisplayName("第三層分類名稱")]
            [StringLength(15, ErrorMessage = "此欄位最長為15字")]
            public string trdLayer { get; set; }

        }

        public partial class MetadataWishList
        {
            [DisplayName("願望清單編號")]
            public string wishID { get; set; }
            [DisplayName("商品編號")]
            public string pdtID { get; set; }
            [DisplayName("會員編號")]
            public string mbrID { get; set; }

        }
    }
}