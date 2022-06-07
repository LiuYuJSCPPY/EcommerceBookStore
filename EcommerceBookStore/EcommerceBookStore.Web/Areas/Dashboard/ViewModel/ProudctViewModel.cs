using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EcommerceBookStore.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace EcommerceBookStore.Web.Areas.Dashboard.ViewModel
{
    public class ProudctListViewModel
    {

        public int Id { get; set; }

        [DisplayName("名字")]
        public string Name { get; set; }

        [DisplayName("作者")]
        public string Author { get; set; }
        [DisplayName("出版社")]
        public string PushlingHouse { get; set; }
        [DisplayName("出版日期")]
        public DateTime PubshDate { get; set; }
        [DisplayName("介紹")]
        public string desc { get; set; }
        [DisplayName("產品庫存")]
        public int ProudctInventory { get; set; }
        [DisplayName("分類")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [DisplayName("價錢")]
        public int price { get; set; }

        [DisplayName("活動")]
        public int? DiscountId { get; set; }
        public Discount discount { get; set; }
        [DisplayName("新增時間")]
        public DateTime Create_at { get; set; }

        [DisplayName("更新時間")]
        public DateTime Modified_at { get; set; }

        [DisplayName("產品圖片")]
        public string ProudctImage { get; set; }
        public IEnumerable<Proudct> proudcts { get; set; }
    }


    public class ProudctViewModel 
    {
        public int Id { get; set; }

        [DisplayName("名字")]

        public string Name { get; set; }

        [DisplayName("作者")]
        public string Author { get; set; }

        [DisplayName("出版社")]
        public string PushlingHouse { get; set; }


        [DisplayFormat(DataFormatString = "{0:d yyyy-MM-dd}")]
        [DisplayName("出版日期")]
        public DateTime PubshDate { get; set; }

        [DisplayName("介紹")]
        public string desc { get; set; }

        [DisplayName("產品庫存")]
        public int ProudctInventory { get; set; }

        [DisplayName("分類")]
        public int CategoryId { get; set; }

        public List<SelectListItem> Category { get; set; }

        [DisplayName("價錢")]
        public int price { get; set; }


        [DisplayName("活動")]
        public int? DiscountId { get; set; }

        public List<SelectListItem> Discount { get; set; }


        [DisplayFormat(DataFormatString = "{0:d yyyy-MM-dd}")]
        [DisplayName("新增時間")]
        public DateTime Create_at { get; set; }



        [DisplayFormat(DataFormatString = "{0:d yyyy-MM-dd}")]
        [DisplayName("更新時間")]
        public DateTime Modified_at { get; set; }



        [DisplayName("產品圖片")]
        public string ProudctImage { get; set; }
    }

}