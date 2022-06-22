using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EcommerceBookStore.Data;
using EcommerceBookStore.Model;


namespace EcommerceBookStore.Web.ViewModel
{
    
    public class ShoppingCartViewModel
    {
        public int CartId { get; set; }
        public Cart cart { get; set; }

        public int ProudctId { get; set; }
        public Proudct proudct { get; set; }

        public int quantity { get; set; }


    }


    public class ShoppingCartListViewModel
    {
        public IEnumerable<CartItem> cartItems { get; set; }
        public Cart cart { get; set; }
    }


    public class CookieCartViewModel
    {
        public IList<ShoppingCartViewModel> shoppingCartViewModels { get; set; }
        public CookieCartViewModel()
        {
            shoppingCartViewModels = new List<ShoppingCartViewModel>();
        }
    }
 
}