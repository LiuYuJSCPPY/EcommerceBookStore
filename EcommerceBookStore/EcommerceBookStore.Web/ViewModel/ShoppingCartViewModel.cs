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
        public int ProudctId { get; set; }
        public int quantity { get; set; }


    }

    public class getAllCartItems
    {
        public int ProudctId { get; set; }
        public Proudct proudct { get; set; }
        public int quantity { get; set; }

        public Discount Discount { get; set; }

    }

    public class CookieCartViewModel
    {
        public IList<ShoppingCartViewModel> shoppingCartViewModels { get; set; }
        public CookieCartViewModel()
        {
            shoppingCartViewModels = new List<ShoppingCartViewModel>();
        }
    }


    public class ShoppingCartListViewModel
    {
        public IEnumerable<CartItem> cartItems { get; set; }
       
    }



  
 
}