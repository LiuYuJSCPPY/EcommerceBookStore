using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceBookStore.Model;
using EcommerceBookStore.Data;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;


using EcommerceBookStore.Server;

namespace EcommerceBookStore.Server
{
    public class OrderSerivice
    {


        public bool SaveOrderItem(String UserId)
        {
            var _Context = new EBookStoreContext();
           
            var cart = _Context.Carts.Where(Id => Id.BookStoreUserId == UserId).First();
            List<CartItem> cartItems = _Context.CartItems.Where(Id => Id.CartId == cart.Id).ToList();


            Order order = _Context.orders.Where(t => t.BookStoreUserId == UserId).First();

            foreach(var Item in cartItems)
            {
                OrderItem orderItem = new OrderItem()
                {
                    OrderId = order.Id,
                    ProudctId = Item.ProudctId,
                    quantity = Item.quantity,
                };
                _Context.orderItems.Add(orderItem);
                
            }
            

            return _Context.SaveChanges() > 0;
        }

    }
}
