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
        public Proudct proudct { get; set; }

        public IEnumerable<ProudctImages> proudctImages { get; set; }
        public IEnumerable<ProudctCommit> proudctCommits { get; set; }

    }

}