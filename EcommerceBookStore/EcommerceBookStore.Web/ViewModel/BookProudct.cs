using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EcommerceBookStore.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EcommerceBookStore.Web.ViewModel
{
    public class BookProudctListView
    {
        public IEnumerable<Proudct> proudcts { get; set; }
        
        public IEnumerable<Category> categories { get; set; }

        public IEnumerable<Discount> discounts { get; set; }
    }
}