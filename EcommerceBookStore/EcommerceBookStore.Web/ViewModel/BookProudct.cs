using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EcommerceBookStore.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using PagedList;
using PagedList.Mvc;

namespace EcommerceBookStore.Web.ViewModel
{
    public class BookProudctListView
    {
        public IEnumerable<Proudct> proudcts { get; set; }
        
        public IEnumerable<Category> categories { get; set; }

        public IEnumerable<Discount> discounts { get; set; }
    }

    public class BookProudctPagation
    {
        public IPagedList<Proudct> proudcts { get; set; }

        public IEnumerable<Category> categories { get; set; }

        public IEnumerable<Discount> discounts { get; set; }
    }

    public class ShopBookDeatil
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string PushlingHouse { get; set; }
        public DateTime PubshDate { get; set; }
        public string desc { get; set; }
        public int ProudctInventory { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int price { get; set; }
        public int? DiscountId { get; set; }
        public Discount discount { get; set; }
        public DateTime Create_at { get; set; }
        public DateTime Modified_at { get; set; }
        public string ProudctImage { get; set; }

        public IEnumerable<ProudctImages> proudctImages { get; set; }
        public IEnumerable<ProudctCommit> proudctCommits { get; set; }

    }

}