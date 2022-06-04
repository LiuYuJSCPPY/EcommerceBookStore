using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EcommerceBookStore.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EcommerceBookStore.Web.Areas.Dashboard.ViewModel
{
    public class CategoryViewListModel
    {
        public IEnumerable<Category> categories { get; set; }
    }
    public class CategoryViewModel
    {

        public int Id { get; set; }
        [Required]
        [DisplayName("名字")]
        public string Name { get; set; }

        [Required]
        [DisplayName("圖片")]
        public string CategroyImage { get; set; }
    }
}