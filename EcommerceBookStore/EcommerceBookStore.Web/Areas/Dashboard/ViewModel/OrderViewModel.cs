using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EcommerceBookStore.Model;
using EcommerceBookStore.Data;
using EcommerceBookStore.Server;


namespace EcommerceBookStore.Web.Areas.Dashboard.ViewModel
{
    public class OrderViewModel
    {
        public Order order { get; set; }
        public IEnumerable<OrderItem> orderItems { get; set; }

    }
}