using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using EcommerceBookStore.Data;
using EcommerceBookStore.Model;
using EcommerceBookStore.Server;
using EcommerceBookStore.Web.Areas.Dashboard.ViewModel;


namespace EcommerceBookStore.Web.Areas.Dashboard.Controllers
{
    public class OrdersController : Controller
    {
        private EBookStoreContext _db = new EBookStoreContext();
        

        // GET: Dashboard/Orders
        public ActionResult Index()
        {
            var order = _db.orders.ToList();
           

            return View(order);
        }
    }
}