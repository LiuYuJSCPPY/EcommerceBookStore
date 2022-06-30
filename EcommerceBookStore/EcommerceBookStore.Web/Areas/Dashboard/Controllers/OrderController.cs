using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using EcommerceBookStore.Model;
using EcommerceBookStore.Data;
using EcommerceBookStore.Server;
using System.Data.Entity;


namespace EcommerceBookStore.Web.Areas.Dashboard.Controllers
{
   
    public class OrderController : Controller
    {
        private EBookStoreContext _db = new EBookStoreContext();
        // GET: Dashboard/Order
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            Cart cart = new Cart();
            cart = _db.Carts.Where(x => x.BookStoreUserId == userId).First();
            List<CartItem> cartItems = _db.CartItems.Where(x => x.CartId == cart.Id).ToList();

            Order order = new Order();
            order.BookStoreUserId = userId;
            _db.orders.Add(order);
            _db.SaveChanges();


            return View();
        }
    }
}