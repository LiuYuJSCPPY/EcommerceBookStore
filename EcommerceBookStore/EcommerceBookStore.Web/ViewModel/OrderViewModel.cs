using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using EcommerceBookStore.Model;

namespace EcommerceBookStore.Web.ViewModel
{
    public class OrderViewModel
    {
    }

    public class CheckOrderItem
    {
        public int CartId { get; set; }
        public IEnumerable<CartItem> CartItems { get; set; }
        public BookStoreUser BookStoreUser { get; set; }

    }
}