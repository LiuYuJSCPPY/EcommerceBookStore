using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EcommerceBookStore.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;



namespace EcommerceBookStore.Web.Areas.Dashboard.ViewModel
{
    public class DiscountViewModel
    {
        public int Id { get; set; }

        [DisplayName("名字")]
        public string Name { get; set; }

        [DisplayName("活動內容")]
        public string Desc { get; set; }

        [DisplayName("活動圖片")]
        public string DiscountImage { get; set; }

        [DisplayName("折價")]
        public decimal Discount_Preceint { get; set; }

        [DisplayName("啟動/關閉")]
        public bool IsActival { get; set; }

        [DisplayName("新增時間")]
        [DisplayFormat(DataFormatString ="{0:dyyyy/MM/dd}",ApplyFormatInEditMode =true)]
        public Nullable<DateTime> Create_at { get; set; }


        [DisplayName("更新時間")]
        [DisplayFormat(DataFormatString = "{0:dyyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> Modified_at { get; set; }


    }

    public class DiscountListViewModel
    {
        public int Id { get; set; }

        [DisplayName("名字")]
        public string Name { get; set; }
        [DisplayName("活動內容")]
        public string Desc { get; set; }
        [DisplayName("活動圖片")]
        public string DiscountImage { get; set; }
        [DisplayName("折價")]
        public decimal Discount_Preceint { get; set; }
        [DisplayName("啟動/關閉")]
        public bool IsActival { get; set; }
        [DisplayName("新增時間")]
        public DateTime Create_at { get; set; }
        [DisplayName("更新時間")]
        public DateTime Modified_at { get; set; }
        public IEnumerable<Discount> discounts { get; set; }
    }
}