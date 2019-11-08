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
        public class MetadataMember
        {
            [DisplayName("會員編號")]
            public string mbrID { get; set; }

            [DisplayName("姓名")]
            [Required(ErrorMessage = "必填")]
            public string mbrName { get; set; }


            [DisplayName("暱稱")]
            [Required(ErrorMessage = "必填")]

            public string nickName { get; set; }

            [DisplayName("電話")]
            public string mbrPhone { get; set; }

            [DisplayName("信箱")]
            [Required(ErrorMessage = "必填")]
            [EmailAddress]
            [DataType(DataType.EmailAddress, ErrorMessage = "請輸入正確的信箱格式!")]
            public string mbrMail { get; set; }


            [DisplayName("生日")]
            [Required(ErrorMessage = "必填")]
            [DataType(DataType.Date, ErrorMessage = "輸入日期有誤")]
            [DisplayFormat(DataFormatString = "{0:yyyy/mm/dd}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> birthDate { get; set; }

            [DisplayName("我的點數")]
            public short Points { get; set; }

            [DisplayName("會員認證")]
            public bool mbrAut { get; set; }

            [DisplayName("註冊日期")]
            public System.DateTime signupDate { get; set; }

            [DisplayName("大頭照")]
            public string mbrImage { get; set; }


        }

        public class MetadataMemberAccount
        {

            [DisplayName("會員帳號")]
            [Required(ErrorMessage = "必填")]
            public string mbrAccount { get; set; }

            [Key]
            [DisplayName("會員密碼")]
            [Required(ErrorMessage = "必填")]
            public string mbrPwd { get; set; }


            [DisplayName("會員編號")]
            public string mbrID { get; set; }

        }


    }
}