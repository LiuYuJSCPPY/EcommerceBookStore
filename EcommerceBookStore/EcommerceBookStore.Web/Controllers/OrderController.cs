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
using EcommerceBookStore.Web.ViewModel;
using System.Data.Entity;

namespace EcommerceBookStore.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        EBookStoreContext _db = new EBookStoreContext();
        private OrderSerivice _orderSerivice = new OrderSerivice();
        // GET: Dashboard/Order
        [Authorize]
        public ActionResult CheckOrderAdmin()
        {
            Cart cart = new Cart();
            CheckOrderItem checkOrderItem = new CheckOrderItem();
            var UserId = User.Identity.GetUserId();


            cart = _db.Carts.Where(x => x.BookStoreUserId == UserId).FirstOrDefault();
            var user = _db.Users.Find(UserId);

            checkOrderItem.CartItems = _db.CartItems.Include(x => x.proudct).Where(x => x.CartId == cart.Id).ToList();
            checkOrderItem.BookStoreUser = user;
            return View(checkOrderItem);
        }


        [HttpPost]
        [Authorize]
        public JsonResult SaveOrder(Order order)
        {
            JsonResult json = new JsonResult();
            bool Result = false;

           
            order.BookStoreUserId = User.Identity.GetUserId();
            _db.orders.Add(order);
            _db.SaveChanges();

            Result = _orderSerivice.SaveOrderItem(User.Identity.GetUserId());


            if (Result)
            {
                json.Data = new { Success = true };

            }
            else
            {
                json.Data = new {Success = false,Message = "Error!!"};
            }



            return json;
        }
    }
}

