using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceBookStore.Data;
using EcommerceBookStore.Model;

namespace EcommerceBookStore.Server
{
   public class CartService
    {

       public Cart GetCartById(string Id)
        {
            var context = new EBookStoreContext();

            return context.Carts.Where(cart => cart.BookStoreUserId == Id).First();
        }


        public bool SaveCart(Cart cart)
        {
            var context = new EBookStoreContext();
            context.Carts.Add(cart);
            
            return context.SaveChanges() > 0;
        }

        public bool SaveCartItem(CartItem cartItem)
        {
            var context = new EBookStoreContext();
            context.CartItems.Add(cartItem);


            return context.SaveChanges() > 0;
        }



    }



   
}
