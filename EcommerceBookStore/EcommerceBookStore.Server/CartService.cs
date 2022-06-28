using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceBookStore.Data;
using EcommerceBookStore.Model;
using System.Data.Entity;

namespace EcommerceBookStore.Server
{
   public class CartService
    {

       public Cart GetCartById(string Id)
        {
            var context = new EBookStoreContext();
           
            return  context.Carts.Where(cart => cart.BookStoreUserId == Id).First();
        }

        public Cart SeachCartId(string Id)
        {
            
            var context = new EBookStoreContext();

            context.Carts.Where(cart => cart.BookStoreUserId == Id).FirstOrDefault();
           
            return context.Carts.Where(cart => cart.BookStoreUserId == Id).FirstOrDefault();
        }


        public CartItem GetCartItemById(int Id)
        {
            var context = new EBookStoreContext();
            return context.CartItems.Find(Id);
        }
        



        //Cart
        public bool SaveCart(Cart cart)
        {
            var context = new EBookStoreContext();
            context.Carts.Add(cart);
            
            return context.SaveChanges() > 0;
        }




        //CartItem
        public bool SaveCartItem(CartItem cartItem)
        {
            var context = new EBookStoreContext();
            context.CartItems.Add(cartItem);


            return context.SaveChanges() > 0;
        }

        public bool UpdateCartItem(CartItem cartItem)
        {
            var context = new EBookStoreContext();
            context.Entry(cartItem).State = EntityState.Modified;
            return context.SaveChanges() > 0;
        }

    }



   
}
