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
           //String UserId = User.Identity.GetUserId();
           // Cart cart = _db.Carts.Where(u => u.BookStoreUserId == UserId).First();
           //if(UserId != null)
           // {
           //     if(cart == null)
           //     {
           //         cart.BookStoreUserId = UserId;
           //         _cartService.SaveCart(cart);
           //     }
           // }

            var CartItems = GetAllCartCookie();
            CartItem cartItem = new CartItem();
            List<getAllCartItems> AllCartItems = new List<getAllCartItems>();
            foreach(var item in CartItems)
            {  
                cartItem.proudct = _db.proudcts.Find(item.ProudctId);
                cartItem.proudct.discount = _db.discounts.Find(cartItem.proudct.DiscountId);
               
                AllCartItems.Add(new getAllCartItems(){
                    ProudctId = item.ProudctId,
                    quantity = item.quantity,
                    proudct = _db.proudcts.Find(item.ProudctId),
                    Discount = cartItem.proudct.discount
                });
            }
           

            return View(AllCartItems);
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
                     Cart SaveCart = new Cart();
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


        [HttpPost]
        public JsonResult UpdateCart(CartItem cartItem,int? Id)
        {
            JsonResult json = new JsonResult();
            string UserId = User.Identity.GetUserId();
            bool result = false;
            
            result = UpdateDbByCart(cartItem,Id.Value);


            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Error" };
            }
            return json;

        }



//Cookie
        [HttpPost]
        public JsonResult UpdateCookieCart(List<ShoppingCartViewModel> CookiesCartItem)
        {
            bool Result = false;
            JsonResult json = new JsonResult();


            Result = SaveCookiesItems(CookiesCartItem);


            if (Result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Error" };
            }

            return json;
        }

        
        [HttpPost]
        public JsonResult DeleteCookieCart(List<ShoppingCartViewModel> CookCartItem)
        {
            bool Result = false;
            JsonResult json = new JsonResult();

            Result = SaveCookiesItems(CookCartItem);


            if (Result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Error" };
            }

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

        private bool SaveCookiesItems (List<ShoppingCartViewModel> shoppingCartViewModels)
        {
            bool Result = false;
            CookieCartViewModel CookiesItems = new CookieCartViewModel();
            CookiesItems.shoppingCartViewModels = shoppingCartViewModels;
            Response.Cookies["Cart"].Value = JsonConvert.SerializeObject(CookiesItems);

            if (Request.Cookies["Cart"] != null)
            {
                Result = true;
            }


            return Result;
        }



        private bool UpdateDbByCart(CartItem cartItem , int Id)
        {
            bool Result = false;
            if (_db.proudcts.Where(x => x.Id == cartItem.ProudctId) != null  && _cartService.GetCartItemById(Id) != null)
            {
                Result = _cartService.UpdateCartItem(cartItem);

            }

            return Result;
        }




        private CookieCartViewModel getCartFormCookie()
        {
            var getCookie = Request.Cookies["Cart"];
            

            CookieCartViewModel CookieCart = new CookieCartViewModel();
            if (getCookie != null)
            {
                string getAllCookie = getCookie.Value;
                CookieCart = JsonConvert.DeserializeObject<CookieCartViewModel>(getAllCookie);
                List<ShoppingCartViewModel> set = CookieCart.shoppingCartViewModels.ToList();

              
            }      

            return CookieCart; 
        }


        private List<ShoppingCartViewModel> GetAllCartCookie()
        {
            var getCookie = Request.Cookies["Cart"];
            var CookieCart = JsonConvert.DeserializeObject<CookieCartViewModel>(getCookie.Value);
            List<ShoppingCartViewModel> AllCart = CookieCart.shoppingCartViewModels.ToList();

            return AllCart;
        }
    }
}