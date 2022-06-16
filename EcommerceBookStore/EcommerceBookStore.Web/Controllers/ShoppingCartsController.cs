using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using EcommerceBookStore.Web.ViewModel;
using EcommerceBookStore.Model;
using EcommerceBookStore.Server;
using EcommerceBookStore.Data;


namespace EcommerceBookStore.Web.Controllers
{
    public class ShoppingCartsController : Controller
    {

        private CartService _cartService;
        private EBookStoreContext _db = new EBookStoreContext();
        // GET: ShoppingCarts
        public ActionResult Index()
        {
            
            return View();
        }



        public JsonResult AddToCartItem(CartItem cartItem)
        {
            JsonResult json = new JsonResult();
            string Id = User.Identity.GetUserId();
            bool result = false;
            if(Id != null)
            {
                Cart IsCart = _cartService.GetCartById(Id);
                if(IsCart == null)
                {
                    var SaveCart = new Cart();
                    SaveCart.BookStoreUserId = Id;
                    if (_cartService.SaveCart(SaveCart))
                    {
                        Cart AddCartId = _cartService.GetCartById(Id);
                       
                        cartItem.CartId = AddCartId.Id;
                        result = _cartService.SaveCartItem(cartItem);
                    }

                }
                else
                {
                   
                    cartItem.CartId = IsCart.Id;
                    result = _cartService.SaveCartItem(cartItem);
                }
            }

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Error" };
            }


           //(會員)如果沒有購物車的話就要新增一個新的購物車放入購物的項目,如果有購物車就保持
           //(非會員)使用Cookies來記住購物車項目。


            return json;
        }

       
    }
}