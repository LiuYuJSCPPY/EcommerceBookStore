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
using Newtonsoft.Json;

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


        public ActionResult CartPartelView()
        {
          

            return PartialView("_CartPartelView");
        }


        [HttpPost]
        public JsonResult AddToCart(CartItem cartItem)
        {
            JsonResult json = new JsonResult();
            string UserId = User.Identity.GetUserId();
            bool result = false;
            if(UserId != null)
            {
                Cart IsCart = _cartService.GetCartById(UserId);
                if(IsCart == null)
                {
                    var SaveCart = new Cart();
                    SaveCart.BookStoreUserId = UserId;
                    if (_cartService.SaveCart(SaveCart))
                    {
                        Cart AddCartId = _cartService.GetCartById(UserId);
                       
                        cartItem.CartId = AddCartId.Id;
                        if(cartItem.quantity == null)
                        {
                            cartItem.quantity++;
                        }
                        
                        result = _cartService.SaveCartItem(cartItem);
                    }

                }
                else
                {
                   
                    cartItem.CartId = IsCart.Id;
                    result = _cartService.SaveCartItem(cartItem);
                }
            }
            else
            {
                result = this.addToCookieCart(cartItem);

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







        private bool addToCookieCart(CartItem cartItem)
        {
            CookieCartViewModel CookiesCart = getCartFormCookie();
           
            bool Result = false;
            if (cartItem != null)
            {
                Proudct FindProudct = _db.proudcts.Find(cartItem.ProudctId);
                if (FindProudct != null)
                {
                    ShoppingCartViewModel cookieCartViewModel = new ShoppingCartViewModel();
                    cookieCartViewModel.ProudctId = cartItem.ProudctId;
                    cookieCartViewModel.quantity = cartItem.quantity;
                    CookiesCart.shoppingCartViewModels.Add(cookieCartViewModel);


                }

            }

            if (SaveCookieCart(CookiesCart))
            {
                Result = true;
                return Result;
            }
            else
            {
                return Result;
            }
        }

        private bool SaveCookieCart(CookieCartViewModel CookieCart)
        {
           
            bool Result = false;


            Response.Cookies["Cart"].Value = JsonConvert.SerializeObject(CookieCart);


            if (Request.Cookies["Cart"] != null)
            {
                Result = true;
            }

            return Result;
        }

        private CookieCartViewModel getCartFormCookie()
        {
            var getCookie = Request.Cookies["Cart"].Value;
            

            CookieCartViewModel CookieCart = new CookieCartViewModel();
            if (getCookie != null)
            {
                string getAllCookie = getCookie;
                 CookieCart = JsonConvert.DeserializeObject<CookieCartViewModel>(getAllCookie);
            }
            
         
            

            return CookieCart;
        }
    }
}